using System;
using System.Net;

using Ostis.Sctp.Responses;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс, реализующий работу с абстрактной базой знаний на сервере.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="KnowledgeBase"/>
    /// <code source="..\Ostis.Tests\ToolsTest.cs" region="KeyNodes" lang="C#" />
    /// </example>
    public class KnowledgeBase:IDisposable
    {
        #region Свойства

        private readonly SctpClient sctpClient;
        private readonly Commands commands;
        private readonly ElementCollection<Node> nodes;
        private readonly ElementCollection<Arc> arcs;
        private readonly ElementCollection<Link> links;

        /// <summary>
        /// Операции с данной базой знаний.
        /// </summary>
        public Commands Commands
        { get { return commands; } } 

        /// <summary>
        /// Список узлов.
        /// </summary>
        public ElementCollection<Node> Nodes
        { get { return nodes; } }

        /// <summary>
        /// Список дуг.
        /// </summary>
        public ElementCollection<Arc> Arcs
        { get { return arcs; } }

        /// <summary>
        /// Список ссылок.
        /// </summary>
        public ElementCollection<Link> Links
        { get { return links; } }

        /// <summary>
        /// Подключение к базе знаний установлено.
        /// </summary>
        public bool IsAvaible
        { get { return sctpClient.IsConnected; } }

        #endregion

        #region Конструкторы

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public KnowledgeBase(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">IP-адрес сервера</param>
        /// <param name="port">номер порта</param>
        public KnowledgeBase(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        /// <summary>
        /// ctor.
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
        /// Сохранение изменений.
        /// </summary>
        public void SaveChanges()
        {
            arcs.SaveChanged();
            nodes.SaveChanged();
            links.SaveChanged();
        }

        /// <summary>
        /// Отправка произвольной синхронной команды на сервер.
        /// </summary>
        /// <param name="command">команда</param>
        /// <returns>полученный ответ</returns>
        public Response ExecuteCommand(Command command)
        {
            Response response = new UnknownResponse(new byte[0]);
            if (sctpClient.IsConnected)
            {
                response = sctpClient.Send(command);
            }
            return response;
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
