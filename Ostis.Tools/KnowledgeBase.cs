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
    public class KnowledgeBase:IDisposable
    {
        private readonly SctpClient sctpClient;
        private readonly Commands commands;

        public Commands Commands
        {
            get { return commands; }
        } 

        private readonly ElementCollection<Node> nodes;

        public ElementCollection<Node> Nodes
        {
            get { return nodes; }
        }

        private readonly ElementCollection<Arc> arcs;

        public ElementCollection<Arc> Arcs
        {
            get { return arcs; }
        }

        private readonly ElementCollection<Link> links;

        public ElementCollection<Link> Links
        {
            get { return links; }
        }

        public void SaveChanges()
        {
            this.arcs.SaveChanged();
            this.nodes.SaveChanged();
            this.links.SaveChanged();
        }
        

        /// <summary>
        /// Возвращает True, если подклучение к базе знаний установлено
        /// </summary>
        public bool IsAvaible 
        {
            get { return sctpClient.IsConnected; }
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
            arcs = new ElementCollection<Arc>(this);
            nodes = new ElementCollection<Node>(this);
            links = new ElementCollection<Link>(this);
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
        ~KnowledgeBase()
        {
            disconnect();
        }

      
        private void disconnect()
        {
            sctpClient.Dispose();
        }

        #endregion
    }


}
