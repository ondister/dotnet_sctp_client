using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
namespace Ostis.Tests
{
    [TestClass]
    public class ArgumentsTest
    {
        [TestMethod]
        [Timeout(3000)]
        public void TestLinkContent()
        {
            LinkContent stringLink = new LinkContent("test и тест");
            Assert.AreEqual("test и тест", LinkContent.ToString(stringLink.Data));

            LinkContent DoubleLink = new LinkContent(123.321d);
            Assert.AreEqual(123.321d, LinkContent.ToDouble(DoubleLink.Data));
        }

        [TestMethod]
        [Timeout(3000)]
        public void TestIdentifier()
        {
            var identifier = new Identifier("new_system_id");
            Assert.AreEqual("new_system_id", identifier.Value);
            Assert.AreEqual("new_system_id", identifier.ToString());
            Identifier nextidentifier = "new_system_id";
            Assert.AreEqual("new_system_id", nextidentifier.Value);
            Assert.AreEqual("new_system_id", nextidentifier.ToString());
        }

       
       
    }
}
