using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp.Tools;
using System.Collections.Generic;
using Ostis.Sctp.Arguments;
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
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.Nodes["nrel_system_identifier"].ScAddress);
            Assert.AreEqual("nrel_system_identifier", knowledgeBase.Nodes["nrel_system_identifier"].SysIdentifier.Value);
            Assert.AreNotEqual(ElementType.Unknown, knowledgeBase.Nodes["nrel_system_identifier"].Type);


        }

        #endregion
        
        #region KeyNodes
        [TestMethod]
        [Timeout(3000)]
        public void TestKeyNodes()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nrel_inclusion"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nrel_system_identifier"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nrel_main_idtf"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("lang_ru"));
            Assert.AreNotEqual(ScAddress.Unknown, knowledgeBase.GetNodeAddress("nlang_en"));

            Assert.AreEqual("nrel_system_identifier", knowledgeBase.GetNodeSysIdentifier(knowledgeBase.GetNodeAddress("nrel_system_identifier")).Value);
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

       
        #region ElementType
        [TestMethod]
        [Timeout(3000)]
        public void TestElementType()
        {
            Assert.IsTrue(KnowledgeBase.CompareElementTypes(ElementType.PositiveConstantPermanentAccessArc, ElementType.PositiveArc));
            Assert.IsTrue(KnowledgeBase.CompareElementTypes(ElementType.ConstantNode, ElementType.Node));
            Assert.IsFalse(KnowledgeBase.CompareElementTypes(ElementType.PositiveArc, ElementType.NegativeArc));
        }
        #endregion
    }
}
