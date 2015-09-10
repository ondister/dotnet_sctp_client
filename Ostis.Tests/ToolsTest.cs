using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp.Tools;
using System.Collections.Generic;
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
            knowledgeBase.GetKeyNodes(new List<string> { "nrel_inclusion" });
            Assert.AreNotEqual(0, knowledgeBase.KeyNodesDictionary.Count);
            Assert.IsTrue(knowledgeBase.KeyNodesDictionary.ContainsKey("nrel_system_identifier"));
            Assert.IsTrue(knowledgeBase.KeyNodesDictionary.ContainsKey("nrel_main_idtf"));
            Assert.IsTrue(knowledgeBase.KeyNodesDictionary.ContainsKey("lang_ru"));
            Assert.IsTrue(knowledgeBase.KeyNodesDictionary.ContainsKey("lang_en"));
            Assert.IsTrue(knowledgeBase.KeyNodesDictionary.ContainsKey("nrel_inclusion"));
            foreach(var keynode in knowledgeBase.KeyNodesDictionary)
            {
                Assert.AreNotEqual(0, keynode.Value.Offset);
            }
        }
        #endregion
    }
}
