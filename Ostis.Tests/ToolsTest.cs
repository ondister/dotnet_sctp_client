using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp.Tools;
using Ostis.Sctp;

namespace Ostis.Tests
{
    [TestClass]
    public class ToolsTest
    {
        #region Node
        [TestMethod]
        [Timeout(3000)]
        public void TestNodes()
        {
            KnowledgeBase kb = new KnowledgeBase(Sctp.SctpProtocol.TestServerIp, Sctp.SctpProtocol.DefaultPortNumber);
            Node n = new Node(Sctp.ElementType.ClassNode_a, "newNode");
            kb.Nodes.Add(n);
            kb.SaveChanges();


        }


        [TestMethod]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestPerfCreateNodesTools()
        {
            KnowledgeBase kb = new KnowledgeBase(SctpProtocol.TestServerIp, SctpProtocol.DefaultPortNumber);
            for (int count = 0; count < 10000; count++)
            {
                Node n = new Node(ElementType.ClassNode_a, "newNode" + count.ToString());
                kb.Nodes.Add(n);
            }

        }
        #endregion

      

    }
}
