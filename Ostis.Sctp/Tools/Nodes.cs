using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    public class Nodes
    {
        private KnowledgeBase knowledgeBase;
        internal Nodes(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }



        public Node this[Identifier sysIdentifier]
        {
            get
            {
                return new Node(knowledgeBase, sysIdentifier);
            }
        }
        public Node this[ScAddress scAddress]
        {
            get
            {
                return new Node(knowledgeBase, scAddress);
            }
        }


        public void Add(ElementType nodeType, Identifier sysIdentifier)
        {
            knowledgeBase.Commands.CreateNode(nodeType, sysIdentifier);
        }


        public void Add(ElementType nodeType, string stringSysIdentifier)
        {
            knowledgeBase.Commands.CreateNode(nodeType, stringSysIdentifier);
        }

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
            Identifier nodeIdtf = knowledgeBase.Commands.FindUniqueSysIdentifier(nodeAddress, nodePreffix);
            knowledgeBase.Commands.SetSysIdentifier(nodeAddress, nodeIdtf);


            return nodeIdtf;
        }

    }
}
