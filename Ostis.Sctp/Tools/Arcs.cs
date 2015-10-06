using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    public class Arcs
    {
        private KnowledgeBase knowledgeBase;
        internal Arcs(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }
       
        
       
        public Arc this[ScAddress scAddress]
        {
            get
            {
                return new Arc(knowledgeBase, scAddress);
            }
        }

        public ScAddress Add(ElementType arcType, IElement beginElement, IElement endElement)
        {
            return knowledgeBase.Commands.CreateArc(arcType, beginElement.ScAddress, endElement.ScAddress);
        }

    }
}
