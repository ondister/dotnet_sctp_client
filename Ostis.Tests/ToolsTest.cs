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
        #endregion

      

    }
}
