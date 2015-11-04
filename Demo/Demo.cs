using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Tools;
using Ostis.Sctp;
using Ostis.Sctp.Arguments;
namespace Demo
{
  public  class Demo
    {

      public void Run()
      {
          KnowledgeBase kb = new KnowledgeBase(SctpProtocol.TestServerIp, SctpProtocol.DefaultPortNumber);

          for (int i = 1; i <= 10; i++)
          {
              Node n = new Node(ElementType.ClassNode_a, (string)i.ToString());
              kb.Nodes.Add(n);
              Console.WriteLine(n.SystemIdentifier);
          }
          Console.WriteLine(kb.Nodes.Count);

      }

    }
}
