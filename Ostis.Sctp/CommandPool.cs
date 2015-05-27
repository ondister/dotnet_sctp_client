using System;
using System.Collections.Generic;

namespace Ostis.Sctp
{
    /// <summary>
    /// Пулл команд для сервера. Соединение с сервером происходит при создании экземпляра класса
    /// </summary>
#warning Реализовать интерфейс IDisposable как рекомендовано в MSDN
#warning Привести к единообразию используемые типы данных - UInt32 к uint
    public class CommandPool : IDisposable
    {
        private readonly List<ACommand> commands;
        private uint counter;
        private readonly IClient client;
        private readonly ResponseFactory responseFactory;

        /// <summary>
        /// Возвращает значение, указывающее подключен ли клиент к серверу
        /// </summary>
        /// <value>
        ///   <c>true</c> если подключен; в противном случае <c>false</c>.
        /// </value>
        public bool Connected
        { get { return client.Connected; } }

        /// <summary>
        /// Инициализирует пулл команд для сервера.
        /// </summary>
        /// <param name="address">Адрес сервера</param>
        /// <param name="port">Порт сервера</param>
        /// <param name="clientType">Тип используемого клиента (синхронный/асинхронный)</param>
        public CommandPool(string address, int port, ClientType clientType)
        {
            client = ClientFactory.CreateClient(clientType);
            client.Received += client_Received;
            client.Connect(address, port);
            commands = new List<ACommand>();
            responseFactory = new ResponseFactory();
            counter = 1;
        }

        private void client_Received(IClient sender, CallBacks.ReceiveEventArgs arg)
        {
            if (arg.ReceivedBytes.Length >= 10)
            {
                AResponse _resp = responseFactory.GetResponse(arg.ReceivedBytes);
                commands.Find(cmd => cmd.Id == _resp.Header.Id).Response = _resp;
                ACommand cmdforremuve = commands.Find(cmd => cmd.Id == _resp.Header.Id);
                commands.Remove(cmdforremuve);
            }
            else
            {
                AResponse _resp = responseFactory.GetResponse(arg.ReceivedBytes);
            }
            if (commands.Count == 0)
            {
                counter = 1;
            }
        }

        /// <summary>
        /// Добавляет команду в поток команд и отправляет ее на сервер
        /// </summary>
        /// <param name="command">Команда</param>
        public void Send(ACommand command)
        {
            command.Id = counter;
            commands.Add(command);
            client.Send(command.BytesStream);
            counter++;
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов. При этом пулл отсоединяется от сервера.
        /// </summary>
        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
