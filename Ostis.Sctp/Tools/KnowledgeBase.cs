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

        private readonly Commands commands;

        public bool IsAvaible 
        {
            get { return sctpClient.IsConnected; }
        }
        public Commands Commands
        {
            get { return commands; }
        } 


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
            sctpClient.Connect();
            nodes = new Tools.Nodes(this);
            links = new Tools.Links(this);
            arcs = new Tools.Arcs(this);
            diagnostic = new Diagnostic(this);
            commands = new Commands(this);
        }

        #endregion


        /// <summary>
        /// Посылает произвольную команду серверу синхронно
        /// </summary>
        /// <param name="command">Команда</param>
        /// <returns></returns>
        public Response ExecuteCommand(Command command)
        {
            Response rsp = new UnknownResponse(new byte[0]);
            if (sctpClient.IsConnected)
            {
                rsp = sctpClient.Send(command);
            }
            return rsp;
        }


    }


}
