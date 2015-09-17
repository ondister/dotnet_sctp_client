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
    }
}
