using System;
using System.Collections.Generic;
using sctp_client.AsyncClient;


namespace sctp_client
{
    /// <summary>
    /// Пулл команд для сервера. Соединение с сервером происходит при создании экземпляра класса
    /// </summary>
   public class CommandPool:IDisposable
    {
       private List<ACommand> _commands;
       private uint _counter;
       private IClient _client;
       private ResponseFactory _rspfactory;

       /// <summary>
       /// Возвращает значение, указывающее подключен ли клиент к серверу
       /// </summary>
       /// <value>
       ///   <c>true</c> если подключен; в противном случае <c>false</c>.
       /// </value>
       public bool Connected
       { 
           get 
           { 
               return _client.Connected; 
           } 
       }

       /// <summary>
       /// Инициализирует пулл команд для сервера.
       /// </summary>
       /// <param name="address">Адрес сервера</param>
       /// <param name="port">Порт сервера</param>
       /// <param name="clienttype">Тип используемого клиента (синхронный/асинхронный)</param>
       public CommandPool(string address, int port,ClientType clienttype)
       {
           _client = ClientFactory.CreateClient(clienttype);
           _client.Received += new CallBacks.ReceiveEventHandler(_client_Received);
           _client.Connect(address, port);
           _commands = new List<ACommand>();
           _rspfactory = new ResponseFactory();
           _counter = 1;
       }

       void _client_Received(IClient sender, CallBacks.ReceiveEventArgs arg)
       {
           if (arg.ReceivedBytes.Length >= 10)
           {
               AResponse _resp = _rspfactory.GetResponse(arg.ReceivedBytes);
               _commands.Find(cmd => cmd.Id == _resp.Header.Id).Response = _resp;
              ACommand cmdforremuve = _commands.Find(cmd => cmd.Id == _resp.Header.Id);
              _commands.Remove(cmdforremuve);
           }
           else
           {
               AResponse _resp = _rspfactory.GetResponse(arg.ReceivedBytes);
           }
           if (_commands.Count == 0)
           {
               _counter = 1;
           }
       }

       /// <summary>
       /// Добавляет команду в поток команд и отправляет ее на сервер
       /// </summary>
       /// <param name="command">Команда</param>
       public void Send(ACommand command)
       {
           command.Id = _counter;
           _commands.Add(command);
           _client.SendBytes(command.BytesStream);
           _counter++;

       }


       /// <summary>
       /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов. При этом пулл отсоединяется от сервера.
       /// </summary>
       public void Dispose()
       {
           _client.Disconnect();
       }
    }
}
