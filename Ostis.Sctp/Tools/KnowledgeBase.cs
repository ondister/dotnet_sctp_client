using System;

using Ostis.Sctp;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;
using System.Net;
using System.Threading;
using System.Collections.Generic;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс, реализующий работу с абстрактной базой знаний на сервере
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="KnowledgeBase"/>
    /// <code source="..\Ostis.Tests\ToolsTest.cs" region="KeyNodes" lang="C#" />
    /// </example>
    public class KnowledgeBase
    {
        private readonly SctpClient sctpClient;
        private Dictionary<String, ScAddress> keyNodesDictionary;

        private readonly Diagnostic diagnostic;

        public Diagnostic Diagnostic
        {
            get { return diagnostic; }
        } 

        
        #region Конструкторы

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public KnowledgeBase(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public KnowledgeBase(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="endPoint">конечная точка подключения на сервере</param>
        public KnowledgeBase(IPEndPoint endPoint)
        {
            sctpClient = new SctpClient(endPoint);
            sctpClient.ResponseReceived += asyncHandler;
            sctpClient.Connect();

            keyNodesDictionary = new Dictionary<string, ScAddress>();

            diagnostic=new Diagnostic(this);
        }

        #endregion


        /// <summary>
        /// Возвращает адрес произвольного узла базы знаний
        /// </summary>
        /// <param name="identifierString">Строка идентификатора</param>
        /// <returns>Адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(string identifierString)
        {
            return GetNodeAddress(new Identifier(identifierString));
        }

        /// <summary>
        /// Возвращает адрес произвольного узла базы знаний
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns>Адрес узла <see cref="ScAddress"/></returns>
        public ScAddress GetNodeAddress(Identifier identifier)
        {
            ScAddress address = ScAddress.Unknown;
            if (sctpClient.IsConnected)
            {
                if (!keyNodesDictionary.ContainsKey(identifier.Value))//если не содержит словарь, то достаем из базы
                {
                    var command = new FindElementCommand(identifier);
                    RunAsyncCommand(command);
                    var response = (FindElementResponse)lastAsyncResponse;
                    keyNodesDictionary.Add(identifier.Value, response.FoundAddress);
                }

                keyNodesDictionary.TryGetValue(identifier.Value, out address);

            }

            return address;
        }


        #region AsyncHandlers
       public void RunAsyncCommand(Command command)
        {
            lastAsyncResponse = null;
            synchronizer.Reset();
            sctpClient.SendAsync(command);
            synchronizer.WaitOne();
        }

        private void asyncHandler(Command command, Response response)
        {
            lastAsyncResponse = response;
            synchronizer.Set();
        }

        private readonly ManualResetEvent synchronizer = new ManualResetEvent(false);
        private Response lastAsyncResponse;

        public Response LastAsyncResponse
        {
            get { return lastAsyncResponse; }
        }
        #endregion
    }


}
