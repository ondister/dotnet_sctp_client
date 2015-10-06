using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
  public class Link:IElement
    {
      


        public LinkContent Content
        {
            get { return knowledgeBase.Commands.GetLinkContent(scAddress); }
        }

        private ScAddress scAddress;

        public ScAddress ScAddress
        {
            get { return scAddress; }
        }

      
      private KnowledgeBase knowledgeBase;

    
      internal Link(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.scAddress = scAddress;
      }

      private Link(KnowledgeBase knowledgeBase)
      {
          this.knowledgeBase = knowledgeBase;
      }




      public ElementType Type
      {
          get { return ElementType.Link_a; }
      }
    }
}
