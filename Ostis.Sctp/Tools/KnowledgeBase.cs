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

        private readonly Diagnostic diagnostic;

        private readonly Nodes nodes;
        private readonly Links links;
        private readonly Arcs arcs;

        public Arcs Arcs
        {
            get { return arcs; }
        }


        public Links Links
        {
            get { return links; }
        }


        public Nodes Nodes
        {
            get { return nodes; }
        } 

        /// <summary>
        /// Возвращает объект, в котором есть методы для диагностики абстрактной базы знаний
        /// </summary>
        /// <value>
        ///  <see cref="Diagnostic"/>
        /// </value>
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
            nodes=new Tools.Nodes(this);
            links = new Tools.Links(this);
            arcs = new Tools.Arcs(this);
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
                var command = new FindElementCommand(identifier);
                RunAsyncCommand(command);
                var response = (FindElementResponse)lastAsyncResponse;
                address = response.FoundAddress;
            }

            return address;
        }

        public Identifier GetNodeSysIdentifier(ScAddress scAddress)
        {
            Identifier sysidtf = "";
            if (sctpClient.IsConnected)
            {
                ConstructionTemplate template = new ConstructionTemplate(scAddress, ElementType.ConstantCommonArc, ElementType.Link, ElementType.PositiveConstantPermanentAccessArc, GetNodeAddress("nrel_system_identifier"));
                var command = new IterateElementsCommand(template);
                RunAsyncCommand(command);
                var response = (IterateElementsResponse)lastAsyncResponse;
                if (response.Constructions.Count == 1)
                {
                    ScAddress link = response.Constructions[0][2];
                    var commandGetLink = new GetLinkContentCommand(link);
                    RunAsyncCommand(commandGetLink);
                    var responseGetLink = (GetLinkContentResponse)lastAsyncResponse;
                    if (responseGetLink.Header.ReturnCode == ReturnCode.Successfull)
                    {
                        sysidtf = LinkContent.ToString(responseGetLink.LinkContent);
                    }
                }
            }

            return sysidtf;
        }

        public ElementType GetElementType(ScAddress scAddress)
        {
            ElementType type = ElementType.Unknown;
            if (sctpClient.IsConnected)
            {
                var command = new GetElementTypeCommand(scAddress);
                RunAsyncCommand(command);
                var response = (GetElementTypeResponse)lastAsyncResponse;
                type = response.ElementType;
            }
            return type;
        }

        public LinkContent GetLinkContent(ScAddress scAddress)
        {
            LinkContent content = new LinkContent("");
            if (sctpClient.IsConnected)
            {
                var command = new GetLinkContentCommand(scAddress);
                RunAsyncCommand(command);
                var response = (GetLinkContentResponse)lastAsyncResponse;
                content = new LinkContent(response.LinkContent);
            }
            return content;
        }

        public static  bool CompareElementTypes(ElementType typeOfElement, ElementType type)
        {
            return ((typeOfElement & type) != 0);

        }


        #region AsyncHandlers
        /// <summary>
        /// Запуск асинхронной команды для базы знаний
        /// </summary>
        /// <param name="command">Команда</param>
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

        /// <summary>
        /// Возвращает последний ответ на асинхронную команду
        /// </summary>
        /// <value>
        /// Ответ
        /// </value>
        public Response LastAsyncResponse
        {
            get { return lastAsyncResponse; }
        }
        #endregion
    }


}
