using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Ostis.Sctp
{
    /// <summary>
    /// OSTIS-клиент, работающий по протоколу SCTP.
    /// </summary>
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
        private uint nextCommandId;

        #endregion

        #region Конструкторы

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public SctpClient(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public SctpClient(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        /// <summary>
        /// ctor.
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
        public Response Send(Command command)
        {
            // установка ID команды
            command.Id = nextCommandId;
            nextCommandId++;
#warning Заменить на Interlocked во имя высшей потокобезопасности!
            
            // отправка запроса
            var bytes = command.GetBytes();
            socket.Send(bytes, bytes.Length, 0);
            
            // приём ответа
            var buffer = new byte[SctpProtocol.DefaultBufferSize];
            using (var stream = new MemoryStream())
            {
                do
                {
                    int receivedBytes = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    stream.Write(buffer, 0, receivedBytes);
                } while (socket.Available > 0);
                return Response.GetResponse(stream.ToArray());
            }
        }

#warning TODO: Создать ответ с ошибкой, который и возвращать при ошибке вызова
#warning TODO: Добавить в синхронный клиент асинхронные методы

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
        /// dtor.
        /// </summary>
        ~SctpClient()
        {
            disconnect();
        }

        bool disposed;

        private void disconnect()
        {
            if (!disposed)
            {
                socket.Disconnect(true);
                socket.Dispose();
                disposed = true;
            }
        }

        #endregion
    }


#warning Удалить это древнее кладбище костылей после реализации асинхронного клиента.
    /*internal interface IClient : IDisposable
    {
        event ReceiveEventHandler Received;

        void Connect(string address, int port);

        void Send(byte[] bytes);

        bool Connected
        { get; }
    }*/

    /*internal class StateObject
    {
        public const int BufferSize = 1024;

        public Socket WorkSocket
        { get; set; }
        
        public byte[] Buffer
        { get; set; }

        public MemoryStream Stream
        { get; set; }

        public StateObject()
        {
            Stream = new MemoryStream();
            Buffer = new byte[BufferSize];
            WorkSocket = null;
        }
    }*/

    /*internal class AsynchronousClient : IClient
    {
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent disconnectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent sendDone = new ManualResetEvent(false);
        private readonly ManualResetEvent receiveDone = new ManualResetEvent(false);

        public event ReceiveEventHandler Received;
        private readonly ReceiveEventArgs receiveArgs;

	    private void raiseReceived()
	    {
	        var handler = Volatile.Read(ref Received);
            if (handler != null)
	        {
                handler(this, receiveArgs);
	        }
	    }

	    private readonly Socket client;
        private StateObject state;

        public AsynchronousClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            receiveArgs = new ReceiveEventArgs();
        }

        public void Connect(string address, int port)
        {
            try
            {
#warning Взятие адреса сделать аналогично синхронному клиенту.
#warning Передача клиента в метод необязательна, так как он и так доступен как поле.
                client.BeginConnect(new IPEndPoint(Dns.GetHostEntry(address).AddressList[0], port), connectCallback, client);
                connectDone.WaitOne();
            }
            catch (Exception exception)
            {
#warning Из этого класса нужно тоже удалить все вызовы консоли.
                Console.WriteLine(exception.ToString());
            }
        }

        private void connectCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = (Socket) asyncResult.AsyncState;
                client.EndConnect(asyncResult);
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);
                connectDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public void Dispose()
        {
#warning Кривая реализация Dispose оставлена умышленно ввиду скорой смерти этого класса.
            try
            {
                client.BeginDisconnect(true, disconnectCallback, client);
                disconnectDone.WaitOne();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void disconnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = (Socket) asyncResult.AsyncState;
                client.EndDisconnect(asyncResult);
                Console.WriteLine("Socket disconnected from {0}", client.RemoteEndPoint);
                disconnectDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public void Send(byte[] bytestosend)
        {
            Send(bytestosend);
            sendDone.WaitOne();
            receive(client);
            receiveDone.WaitOne();
        }

        private void sendAsync(byte[] bytes)
        {
            client.BeginSend(bytes, 0, bytes.Length, 0, sendCallback, client);
        }

        private void sendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                sendDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        
        private void receive(Socket client)
        {
            try
            {
                state = new StateObject { WorkSocket = client };
                state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, receiveCallback, state);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void receiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                var state = (StateObject) asyncResult.AsyncState;
                int bytesRead = state.WorkSocket.EndReceive(asyncResult);
                if (bytesRead > 0)
                {
                   state.Stream.Write(state.Buffer, 0, bytesRead);
                }

                if (bytesRead == StateObject.BufferSize)
                {
                    state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, receiveCallback, state);
                }
                else
                {
                    state.Stream.Close();
                    receiveArgs.ReceivedBytes = state.Stream.ToArray();
                    receiveDone.Set();
                    raiseReceived();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool Connected
        { get { return client.Connected; } }
    }*/

    /*public class CommandPool : IDisposable
    {
        private readonly List<Command> commands;
        private uint counter;
        private readonly IClient client;

        /// <summary>
        /// Подключен ли клиент к серверу.
        /// </summary>
        /// <value><b>true</b> если подключен; в противном случае <b>false</b>.</value>
        public bool IsConnected
        { get { return client.Connected; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">IP-адрес</param>
        /// <param name="port">номер порта</param>
        /// <param name="asyncClient">тип используемого клиента (синхронный/асинхронный)</param>
        public CommandPool(string address, int port, bool asyncClient)
        {
            client = asyncClient ? new AsyncClient.AsynchronousClient() as IClient : new SctpClient();
            client.Received += client_Received;
            client.Connect(address, port);
            commands = new List<Command>();
            counter = 1;
        }

        private void client_Received(IClient sender, CallBacks.ReceiveEventArgs arg)
        {
            var response = Response.GetResponse(arg.ReceivedBytes);
            commands.Find(cmd => cmd.Id == response.Header.Id).Response = response;
            commands.Remove(commands.Find(cmd => cmd.Id == response.Header.Id));
#warning здесь часом не должно было вызываться событие прихода ответа на команду?
            if (commands.Count == 0)
            {
#warning Очень, очень скользкое место - обнуление счётчика команд.
                counter = 1;
            }
        }

        /// <summary>
        /// Добавление команды в поток команд и отправкат её на сервер.
        /// </summary>
        /// <param name="command">команда</param>
        public void Send(Command command)
        {
            command.Id = counter;
            commands.Add(command);
            client.Send(command.GetBytes());
            counter++;
        }

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
        /// dtor.
        /// </summary>
        ~CommandPool()
        {
            disconnect();
        }

        bool disposed;

        private void disconnect()
        {
            if (!disposed)
            {
                client.Dispose();
                disposed = true;
            }
        }

        #endregion
    }*/

    /*internal delegate void ReceiveEventHandler(IClient sender, ReceiveEventArgs arg);

    internal class ReceiveEventArgs : EventArgs
    {
        public byte[] ReceivedBytes
        { get; set; }
    }*/

    /*    public delegate void CommandDoneEventHandler(Command sender, CommandDoneEventArgs arg);

    /// <summary>
    /// Данные события завершения исполнения команды.
    /// </summary>
    public class CommandDoneEventArgs : EventArgs
    {
        /// <summary>
        /// Код завешения.
        /// </summary>
        public ReturnCode ReturnCode 
        { get; set; }
    }*/
}
