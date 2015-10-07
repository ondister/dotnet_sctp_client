using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Ostis.Sctp
{
    /// <summary>
    /// OSTIS-клиент, работающий по протоколу SCTP.
    /// </summary>
    /// <remarks>
    /// Клиент реализован по описанию, которое взято <a href="https://github.com/deniskoronchik/sc-machine/wiki/sctp"> в вики репозитория sc-memory</a>
    ///<para>
    ///Клиент работает как в синхронном, так и в асинхронном режиме. Несмотря на то, что сервер работает только в синхронном режиме, рекомендуется использовать асинхронный клиент. Он тупо быстрее в реальных приложениях из-за отсутствия блокировки при ожидании ответа от сервера.
    ///</para>
    /// <example>
    /// Следующий пример демонстрирует использование класса для подключения к серверу:
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="Connect" lang="C#" />
    /// </example>
    /// <example>
    /// Следующий пример содержит необходимую часть кода, если планируется использовать асинхронный клиент
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="AsyncHandlers" lang="C#" />
    /// </example>
    /// </remarks>

    public class SctpClient : IDisposable
    {
        #region Свойства

        /// <summary>
        /// Адрес сервера.
        /// </summary>
        public IPEndPoint ServerEndPoint
        { get { return endPoint; } }

        /// <summary>
        /// Клиент подключён.
        /// </summary>
        public bool IsConnected
        { get { return socket.Connected; } }

        private readonly IPEndPoint endPoint;
        private readonly Socket socket;
        private long nextCommandId;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public SctpClient(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public SctpClient(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="endPoint">конечная точка подключения на сервере</param>
        public SctpClient(IPEndPoint endPoint)
        {
            nextCommandId = 1;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.endPoint = endPoint;
        }

        #endregion

        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        public void Connect()
        {
            socket.Connect(endPoint);
        }

        /// <summary>
        /// Отправка команды на сервер синхронно.
        /// </summary>
        /// <param name="command">команда</param>
        /// <returns>ответ сервера</returns>
        public Response Send(Command command)
        {

            // установка ID команды
#warning Здесь кроется потенциальная ошибка с приведением типов и переполнением.
            command.Id = (uint)Interlocked.Increment(ref nextCommandId);

            // отправка запроса
            var bytes = command.GetBytes();
            socket.Send(bytes, bytes.Length, 0);
            // приём ответа
            var buffer = new byte[SctpProtocol.DefaultBufferSize];
            using (var stream = new MemoryStream())
            {
#warning Здесь ошибка, приводящая к обрезке данных если буфер меньше передаваемых данных при синхронной передаче. Проявляется при итерировании элементов, если возвращается большое количество элементов
                do
                {
                    int receivedBytes = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    stream.Write(buffer, 0, receivedBytes);
#warning Костыль для ошибки
                    if (socket.Available > 0) { Thread.Sleep(TimeSpan.FromMilliseconds(0.6)); }
                } while (socket.Available > 0);

                return Response.GetResponse(stream.ToArray());
            }

        }

        #region Асинхронная отправка

#warning Проверить, не потребуется ли ограничивать доступ к сокету одновременно только одному потоку?
#warning Перечитать Рихтера, чтобы выбрать для синхронизации максимально быстрые примитивы синхронизации.

        /// <summary>
        /// Отправка команды на сервер асинхронно.
        /// </summary>
        /// <param name="command">команда</param>
        public void SendAsync(Command command)
        {
            // установка ID команды
#warning Аналогично про приведение типов и переполнение.
            command.Id = (uint)Interlocked.Increment(ref nextCommandId);
            var bytes = command.GetBytes();
            var state = new StateObject(socket);
            state.Done.Reset();
            socket.BeginSend(bytes, 0, bytes.Length, 0, sendCallback, state);
            state.Done.WaitOne();

            // приём ответа
            state.Done.Reset();
            socket.BeginReceive(state.TempBuffer, 0, SctpProtocol.DefaultBufferSize, 0, asyncResult =>
            {
                receiveCallback(asyncResult);
                state.Done.WaitOne();
                var handler = Volatile.Read(ref ResponseReceived);
                if (handler != null)
                {
                    handler(command, Response.GetResponse(state.Buffer.ToArray()));
                }
            }, state);
        }

#warning Если ответы от сервера приходят в непредсказуемом порядке, то имеет смысл сделать ID команды INTERNAL и не светить его нигде, а при получении ответа сверяться по ID,
        // что именно нам пришло в ответе. Это решит проблему с исчерпанием ID-шников (по крайней мере, снизит вероятность до 0,000...1).
        // Таким образом, клиент на 100% превратится в асинхронный. Тут надо будет продумать потокобезопасность очень серьёзно.
        // А ещё придётся запариться, чтобы разделять, где заканчивается один ответ и начинается следующий.

        /// <summary>
        /// Обработчик получения ответа на команду.
        /// </summary>
        /// <param name="command">отправленная команда</param>
        /// <param name="response">полученный</param>
        public delegate void SctpResponseHandler(Command command, Response response);

        /// <summary>
        /// Получен ответ на асинхронную команду.
        /// </summary>
        public event SctpResponseHandler ResponseReceived;

        private static void sendCallback(IAsyncResult asyncResult)
        {
            var client = (StateObject)asyncResult.AsyncState;
            client.Socket.EndSend(asyncResult);
            client.Done.Set();
        }

        private static void receiveCallback(IAsyncResult asyncResult)
        {
            var state = (StateObject)asyncResult.AsyncState;
            int bytesRead = state.Socket.EndReceive(asyncResult);
            if (bytesRead > 0)
            {
                state.UpdateBuffers(bytesRead);
                if (state.Socket.Available > 0)
                {
                    state.Socket.BeginReceive(state.TempBuffer, 0, SctpProtocol.DefaultBufferSize, 0, receiveCallback, state);
                }
                else
                {
                    state.Done.Set();
                }
            }
        }

        private class StateObject
        {
            #region Data

            public readonly Socket Socket;

            public readonly List<byte> Buffer;

            public byte[] TempBuffer
            { get; private set; }

            public readonly ManualResetEvent Done;

            #endregion

            public StateObject(Socket socket)
            {
                Socket = socket;
                Buffer = new List<byte>();
                TempBuffer = new byte[SctpProtocol.DefaultBufferSize];
                Done = new ManualResetEvent(false);
            }

            public void UpdateBuffers(int bytesRead)
            {
                Buffer.AddRange(TempBuffer.Take(bytesRead));
                TempBuffer = new byte[SctpProtocol.DefaultBufferSize];
            }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            disconnect();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~SctpClient()
        {
            disconnect();
        }

        bool disposed;

        private void disconnect()
        {
            if (IsConnected)
            {
                if (!disposed)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                disposed = true;
            }
        }

        #endregion
    }
}
