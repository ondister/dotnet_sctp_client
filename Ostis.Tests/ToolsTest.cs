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
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Nodes["nrel_system_identifier"].ScAddress);
            Assert.AreEqual("nrel_system_identifier", knowledgeBase.Nodes["nrel_system_identifier"].SysIdentifier.Value);
            Assert.AreNotEqual(ElementType.Unknown, knowledgeBase.Nodes["nrel_system_identifier"].Type);
            Assert.AreEqual(ScAddress.Invalid, knowledgeBase.Nodes["sdsdsdsdsd"].ScAddress);
            Assert.AreEqual(Identifier.Invalid, knowledgeBase.Nodes["sdsdsdsdsd"].SysIdentifier);
            Assert.AreEqual(ElementType.Unknown, knowledgeBase.Nodes["sdsdsdsdsd"].Type);
            ScAddress nodeAddress = knowledgeBase.Nodes.Add(ElementType.ConstantNode_c, "new_node");
            Assert.AreEqual(nodeAddress, knowledgeBase.Nodes["new_node"].ScAddress);

        }

        #endregion

        #region Node
        [TestMethod]
        [Timeout(3000)]
        public void TestCreateNonAtomicNodes()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            ElementType nodeType = ElementType.Node_a;
            nodeType = nodeType.AddType(ElementType.Constant_a);
            knowledgeBase.Nodes.Add(nodeType, "test_node");
            Assert.AreEqual(ElementType.ConstantNode_c, knowledgeBase.Nodes["test_node"].Type);

        }

        #endregion




        #region UniqueNode
        [TestMethod]
        public void TestUniqueNodes()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            for (int count = 0; count < 1000; count++)
            {
                knowledgeBase.Nodes.AddUnique(ElementType.ConstantNode_c, "preffix");
            }

        }

        #endregion


        #region CreateArc
        [TestMethod]
        public void TestCreateArc()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            for (int count = 0; count < 1000; count++)
            {
                Identifier beginNodeId = knowledgeBase.Nodes.AddUnique(ElementType.ClassNode_a, "begin");
                Identifier endNodeId = knowledgeBase.Nodes.AddUnique(ElementType.ConstantNode_c, "end");
                knowledgeBase.Arcs.Add(ElementType.PositiveConstantPermanentAccessArc_c, knowledgeBase.Nodes[beginNodeId], knowledgeBase.Nodes[endNodeId]);

            }

        }

        #endregion



        #region KeyNodes
        [TestMethod]
        [Timeout(3000)]
        public void TestKeyNodes()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("nrel_inclusion"));
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("nrel_system_identifier"));
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("nrel_main_idtf"));
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("lang_ru"));
            Assert.AreNotEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("lang_en"));
            Assert.AreEqual(ScAddress.Invalid, knowledgeBase.Commands.GetNodeAddress("111111lang_ennnnnnnnnn"));
            Assert.AreEqual("nrel_system_identifier", knowledgeBase.Commands.GetNodeSysIdentifier(knowledgeBase.Commands.GetNodeAddress("nrel_system_identifier")).Value);
        }
        #endregion

        #region Diagnostic
        [TestMethod]
        public void TestGetNodesWithoutMainIdtf()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            knowledgeBase.Diagnostic.GetNodesWithoutMainIdtf("NodesWithoutMainIdtf.txt");
        }

        [TestMethod]
        public void TestGetNodesWithoutInputArcs()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            knowledgeBase.Diagnostic.GetNodesWithoutInputArcs("NodesWithoutInputsArcs.txt");
        }
        #endregion

        #region CreateArc
        [TestMethod]
        [Timeout(3000)]
        public void TestEqualElements()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            Assert.IsTrue(knowledgeBase.Nodes["lang_ru"] == knowledgeBase.Nodes["lang_ru"]);
            Assert.IsFalse(knowledgeBase.Nodes["lang_ru"] == knowledgeBase.Nodes["lang_en"]);
            Assert.IsFalse(knowledgeBase.Nodes["lang_ru"] == knowledgeBase.Nodes[ScAddress.Invalid]);
        }

        #endregion

    }
}
