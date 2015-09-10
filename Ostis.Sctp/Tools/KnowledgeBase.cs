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
    /// Класс, реализующий работу с некой базой знаний на сервере
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="KnowledgeBase"/>
    /// <code source="..\Ostis.Tests\ToolsTest.cs" region="KeyNodes" lang="C#" />
    /// </example>
    public class KnowledgeBase
    {
        private readonly SctpClient sctpClient;
        private Dictionary<String, ScAddress> keyNodesDictionary;
        private readonly List<String> nodesList;
        /// <summary>
        /// Возвращает словарь с ключевыми узлами базы знаний
        /// </summary>
        /// <value>
        /// Наименование и адрес ключевых узлов базы знаний
        /// </value>
        public Dictionary<String, ScAddress> KeyNodesDictionary
        {
            get { return keyNodesDictionary; }
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
            this.GetKeyNodes();

        }

        #endregion
        #region keynodes
        public void GetKeyNodes(List<String> nodesList)
        {
            if (sctpClient.IsConnected)
            {
                //set the main nodes
                List<String> listNodes = new List<string>();
                listNodes.Add("nrel_system_identifier");
                listNodes.Add("nrel_main_idtf");
                listNodes.Add("lang_ru");
                listNodes.Add("lang_en");
                listNodes.AddRange(nodesList);
                //find them
                foreach (var keynode in listNodes)
                {
                    var command = new FindElementCommand(new Identifier(keynode));
                    runAsyncCommand(command);
                    var response = (FindElementResponse)lastAsyncResponse;
                    if (response.Header.ReturnCode == ReturnCode.Successfull & !keyNodesDictionary.ContainsKey(keynode))
                    {
                        
                        keyNodesDictionary.Add(keynode, response.FoundAddress);
                    }
                }

            }
        }
        private void GetKeyNodes()
        {
            this.GetKeyNodes(new List<String> { });
        }
        
        
        public ScAddress GetKeyNode(string identifier)
        {
            ScAddress address = new ScAddress(0, 0);
            keyNodesDictionary.TryGetValue(identifier, out address);
            return address;
        }
        #endregion
        #region AsyncHandlers
        private void runAsyncCommand(Command command)
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
        #endregion
    }


}
