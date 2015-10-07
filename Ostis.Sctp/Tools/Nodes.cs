using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
   /// <summary>
   /// Класс, инкапсулирующий коллекцию узлов базы знаний
   /// </summary>
   /// <remarks>
   /// Сам по себе класс не содержит никаких кешируемых данных, так как данные узлов могут меняться, а постоянно запрашивает необходимую информацию из базы знаний
   /// </remarks>
    public class Nodes
    {
        private KnowledgeBase knowledgeBase;
        internal Nodes(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }


        /// <summary>
        /// Возвращает узел по его системному идентификатору
        /// </summary>
        /// <param name="sysIdentifier">Системный идентификатор узла</param>
        /// <returns>Найденный узел</returns>
        public Node this[Identifier sysIdentifier]
        {
            get
            {
                return new Node(knowledgeBase, sysIdentifier);
            }
        }

        /// <summary>
        /// Возвращает узел по известному адресу
        /// </summary>
        /// <param name="scAddress">Адрес узла</param>
        /// <returns>Найденный узел</returns>
        public Node this[ScAddress scAddress]
        {
            get
            {
                return new Node(knowledgeBase, scAddress);
            }
        }


        /// <summary>
        /// Добавляет в базу знаний узел определенного типа с указанным идентификатором
        /// </summary>
        /// <param name="nodeType">Тип узла</param>
        /// <param name="sysIdentifier">Системный идентификатор</param>
        public ScAddress Add(ElementType nodeType, Identifier sysIdentifier)
        {
           return  knowledgeBase.Commands.CreateNode(nodeType, sysIdentifier);
        }


        /// <summary>
        /// Добавляет в базу знаний узел определенного типа
        /// </summary>
        /// <param name="nodeType">Тип узла</param>
        public void Add(ElementType nodeType)
        {
            knowledgeBase.Commands.CreateNode(nodeType);
        }

        /// <summary>
        /// Добавляет в базу новый узел с уникальным идентификатором
        /// </summary>
        /// <param name="nodeType">Тип узла</param>
        /// <param name="nodePreffix">Преффикс для узла</param>
        /// <returns>Идентификатор, присвоенный узлу</returns>
        public Identifier AddUnique(ElementType nodeType, string nodePreffix)
        {
            ScAddress nodeAddress = knowledgeBase.Commands.CreateNode(nodeType);
            Identifier nodeIdtf = knowledgeBase.Commands.GenerateUniqueSysIdentifier(nodeAddress, nodePreffix);
            knowledgeBase.Commands.SetSysIdentifier(nodeAddress, nodeIdtf);


            return nodeIdtf;
        }

    }
}
