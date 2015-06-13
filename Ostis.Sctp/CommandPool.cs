using System;
using System.Collections.Generic;

namespace Ostis.Sctp
{
    /// <summary>
    /// Пулл команд для сервера (авто-соединение с сервером).
    /// </summary>
#warning Удалить этот класс - вся функциональность должна быть реализована в клиенте.
    public class CommandPool : IDisposable
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
            client = asyncClient ? new AsyncClient.AsynchronousClient() as IClient : new SyncClient.SynchronousClient();
            client.Received += client_Received;
            client.Connect(address, port);
            commands = new List<Command>();
            counter = 1;
        }

        private void client_Received(IClient sender, CallBacks.ReceiveEventArgs arg)
        {
#warning Что означает магическое число 10?
            if (arg.ReceivedBytes.Length >= 10)
            {
                var response = Response.GetResponse(arg.ReceivedBytes);
                commands.Find(cmd => cmd.Id == response.Header.Id).Response = response;
                commands.Remove(commands.Find(cmd => cmd.Id == response.Header.Id));
            }
            else
            {
#warning Куда идёт это значение?
                Response response = Response.GetResponse(arg.ReceivedBytes);
            }
            if (commands.Count == 0)
            {
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
    }
}
