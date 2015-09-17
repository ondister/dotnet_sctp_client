using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    public class Links
    {
        private KnowledgeBase knowledgeBase;
        internal Links(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }
       
        
       
        public Link this[ScAddress scAddress]
        {
            get
            {
                return new Link(knowledgeBase, scAddress);
            }
        }
    }
}
