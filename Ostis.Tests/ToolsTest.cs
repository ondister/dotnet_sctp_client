using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp.Tools;
using System.Collections.Generic;
using Ostis.Sctp.Arguments;
namespace Ostis.Tests
{
    [TestClass]
    public class ToolsTest
    {
        #region KeyNodes
        [TestMethod]
        [Timeout(3000)]
        public void TestKeyNodes()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.AreNotEqual(ScAddress.Unknown,knowledgeBase.GetNodeAddress("nrel_inclusion"));
            Assert.AreNotEqual(ScAddress.Unknown,knowledgeBase.GetNodeAddress("nrel_system_identifier"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nrel_main_idtf"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("lang_ru"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nlang_en"));
        }
        #endregion

        #region Diagnostic
        [TestMethod]
        [Timeout(3000)]
        public void TestGetNodesWithoutMainIdtf()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            knowledgeBase.Diagnostic.GetNodesWithoutMainIdtf("NodesWithoutMainIdtf.txt");
        }

        [TestMethod]
        [Timeout(3000)]
        public void TestGetNodesWithoutInputArcs()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            knowledgeBase.Diagnostic.GetNodesWithoutInputArcs("NodesWithoutInputsArcs.txt");
        }
        #endregion
    }
}
