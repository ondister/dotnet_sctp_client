using System;
using System.Collections.Generic;

namespace Ostis.Sctp
{
    /// <summary>
    /// Пулл команд для сервера (авто-соединение с сервером).
    /// </summary>
#warning Реализовать интерфейс IDisposable как рекомендовано в MSDN
#warning Привести к единообразию используемые типы данных - UInt32 к uint
    public class CommandPool : IDisposable
    {
        private readonly List<Command> commands;
        private uint counter;
        private readonly IClient client;
        private readonly ResponseFactory responseFactory;

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
        /// <param name="clientType">тип используемого клиента (синхронный/асинхронный)</param>
        public CommandPool(string address, int port, ClientType clientType)
        {
            client = ClientFactory.CreateClient(clientType);
            client.Received += client_Received;
            client.Connect(address, port);
            commands = new List<Command>();
            responseFactory = new ResponseFactory();
            counter = 1;
        }

        private void client_Received(IClient sender, CallBacks.ReceiveEventArgs arg)
        {
#warning Что означает магическое число 10?
            if (arg.ReceivedBytes.Length >= 10)
            {
                var response = responseFactory.GetResponse(arg.ReceivedBytes);
                commands.Find(cmd => cmd.Id == response.Header.Id).Response = response;
                commands.Remove(commands.Find(cmd => cmd.Id == response.Header.Id));
            }
            else
            {
#warning Куда идёт это значение?
                Response response = responseFactory.GetResponse(arg.ReceivedBytes);
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
            client.Send(command.BytesStream);
            counter++;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
