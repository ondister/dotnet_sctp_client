using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;

namespace Ostis.Sctp.Tools
{
    public class Arc : IElement
    {

        private readonly IElement beginElement;

        public IElement BeginElement
        {
            get { return beginElement; }
        }


        private readonly IElement endElement;

        public IElement EndElement
        {
            get { return endElement; }
        } 


      
        public ElementType Type
        {
            get { return knowledgeBase.Commands.GetElementType(address); }
        }

        private ScAddress address;

        public ScAddress ScAddress
        {
            get { return address; }
        }

      
      private KnowledgeBase knowledgeBase;

    
      internal Arc(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.address = scAddress;

          var command = new GetArcElementsCommand(scAddress);
          var response = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(command);

          if (knowledgeBase.Commands.GetElementType(response.BeginElementAddress) == ElementType.Link_a)
          { beginElement = knowledgeBase.Links[response.BeginElementAddress]; }
          else
          { beginElement = knowledgeBase.Nodes[response.BeginElementAddress]; }

          if (knowledgeBase.Commands.GetElementType(response.EndElementAddress) == ElementType.Link_a)
          { endElement = knowledgeBase.Links[response.EndElementAddress]; }
          else
          { endElement = knowledgeBase.Nodes[response.EndElementAddress]; }
          
          
      }

      private Arc(KnowledgeBase knowledgeBase)
      {
          this.knowledgeBase = knowledgeBase;
      }



    
    }
}
