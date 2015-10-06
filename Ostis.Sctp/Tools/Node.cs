using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
  public class Node:IElement
    {
      

        private Identifier sysIdentifier;

        public Identifier SysIdentifier
        {
            get { return knowledgeBase.Commands.GetNodeSysIdentifier(scAddress); }
        }

        private ScAddress scAddress;

        public ScAddress ScAddress
        {
            get { return scAddress; }
        }


        public ElementType Type
        {
            get { return knowledgeBase.Commands.GetElementType(scAddress); }
        }

      private KnowledgeBase knowledgeBase;

      internal Node(KnowledgeBase knowledgeBase,Identifier sysIdentifier)
          :this(knowledgeBase)
      {
          this.sysIdentifier = sysIdentifier;
          this.scAddress = knowledgeBase.Commands.GetNodeAddress(sysIdentifier);
      }

      internal Node(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.scAddress = scAddress;
      }

      private Node(KnowledgeBase knowledgeBase)
      {
          this.knowledgeBase = knowledgeBase;
      }


    }
}
