using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Tools; // пространство имен аргументов команд
namespace Ostis.Tests
{
    [TestClass]
    public class ArgumentsTest
    {
        #region LinkContent
        [TestMethod]
        [Timeout(3000)]
        public void TestLinkContent()
        {
            LinkContent stringLink = new LinkContent("test и тест");
            Assert.AreEqual("test и тест", LinkContent.ToString(stringLink.Data));

            LinkContent DoubleLink = new LinkContent(123.321d);
            Assert.AreEqual(123.321d, LinkContent.ToDouble(DoubleLink.Data));
        }
        #endregion

        #region Identifier
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
            Assert.AreEqual(identifier, nextidentifier);
            Assert.IsTrue(identifier == nextidentifier);
            Assert.IsFalse(identifier != nextidentifier);
            nextidentifier = null;
            Assert.IsTrue(identifier != null);
        }

        #endregion

        #region ElementType
        [TestMethod]
        [Timeout(3000)]
        public void TestElementType()
        {
          ElementType elementType= ElementType.Node_a;
          elementType = elementType.AddType(ElementType.Constant_a);
          Assert.AreEqual(ElementType.ConstantNode_c, elementType);
          Assert.IsTrue(elementType.IsType(ElementType.Constant_a));
          Assert.IsTrue(elementType.HasAnyType(ElementType.AnyElementMask_c));
          elementType = elementType.RemoveType(ElementType.Constant_a);
          Assert.IsFalse(elementType.IsType(ElementType.Constant_a));
          Assert.IsTrue(elementType.IsType(ElementType.Node_a));
          Assert.IsTrue(ElementType.PositiveArc_a.HasAnyType(ElementType.PositiveConstantPermanentAccessArc_c));
          Assert.IsTrue(ElementType.ConstantNode_c.IsType(ElementType.Node_a));
          Assert.IsFalse(ElementType.PositiveArc_a.IsType(ElementType.NegativeArc_a));

        }

        #endregion

        #region ConstructionTemplate
        [TestMethod]
        [Timeout(3000)]
        public void TestConstructionTemplate()
        {
            var elementNode = ElementType.ConstantNode_c;
            var elementArcCommon = ElementType.ConstantCommonArc_c;
            var elementArcAccess = ElementType.PositiveConstantPermanentAccessArc_c;
            var scAddress1 = new ScAddress(1, 1);
            var scAddress2 = new ScAddress(1, 2);
            var scAddress3 = new ScAddress(1, 3);

            //a_a_f
            var constructiontemplate_a_a_f = new ConstructionTemplate(elementNode, elementArcAccess, scAddress1);
            var bytes = constructiontemplate_a_a_f.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //f_a_a
            var constructiontemplate_f_a_a = new ConstructionTemplate(scAddress1, elementArcAccess, elementArcCommon);
            bytes = constructiontemplate_f_a_a.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //f_a_f
            var constructiontemplate_f_a_f = new ConstructionTemplate(scAddress1, elementArcAccess, scAddress2);
            bytes = constructiontemplate_f_a_f.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //a_a_f_a_a
            var constructiontemplate_a_a_f_a_a = new ConstructionTemplate(elementNode, elementArcCommon, scAddress1, elementArcAccess, elementNode);
            bytes = constructiontemplate_a_a_f_a_a.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //a_a_f_a_f
            var constructiontemplate_a_a_f_a_f = new ConstructionTemplate(elementNode, elementArcCommon, scAddress1, elementArcAccess, scAddress2);
            bytes = constructiontemplate_a_a_f_a_f.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //f_a_a_a_a
            var constructiontemplate_f_a_a_a_a = new ConstructionTemplate(scAddress1, elementArcCommon, elementNode, elementArcAccess, elementNode);
            bytes = constructiontemplate_f_a_a_a_a.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //f_a_a_a_f
            var constructiontemplate_f_a_a_a_f = new ConstructionTemplate(scAddress1, elementArcCommon, elementNode, elementArcAccess, scAddress2);
            bytes = constructiontemplate_f_a_a_a_f.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

            //f_a_f_a_a
            var constructiontemplate_f_a_f_a_a = new ConstructionTemplate(scAddress1, elementArcCommon, scAddress2, elementArcAccess, elementNode);
            bytes = constructiontemplate_f_a_f_a_a.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);


            //f_a_f_a_f
            var constructiontemplate_f_a_f_a_f = new ConstructionTemplate(scAddress1, elementArcCommon, scAddress2, elementArcAccess, scAddress3);
            bytes = constructiontemplate_f_a_f_a_f.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);

        }
        #endregion

        #region ScAddress
        [TestMethod]
        [Timeout(3000)]
        public void TestScAddress()
        {
            //new scaddress
            ScAddress scAddress = new ScAddress(1, 2);
            ScAddress nextscAddress = new ScAddress(1, 2);
            //get  bytes
            var bytes = scAddress.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);
            byte[] bytesAddress = new byte[4] { 1, 0, 2, 0 };
            //new scaddress from bytes
            ScAddress scAddressFromBytes = ScAddress.Parse(bytesAddress, 0);
            Assert.AreEqual(scAddress.Segment, scAddressFromBytes.Segment);
            Assert.AreEqual(scAddress.Offset, scAddressFromBytes.Offset);
            Assert.AreEqual(scAddress, nextscAddress);
            Assert.IsTrue(scAddress == nextscAddress);
            Assert.IsFalse(scAddress != nextscAddress);
            nextscAddress = null;
            Assert.IsTrue(scAddress != nextscAddress);
        }
        #endregion

        #region ScEvent
        [TestMethod]
        [Timeout(3000)]
        public void TestScEvent()
        {
            //new scEvent
            ScEvent scEvent = new ScEvent(new SubscriptionId(1), new ScAddress(1, 2), new ScAddress(1, 3));
            //get  bytes
            var bytes = scEvent.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);
            byte[] bytesEvent = new byte[12] { 1, 0, 0, 0, 1, 0, 2, 0, 1, 0, 3, 0 };
            //new scEvent from bytes
            ScEvent scEventFromBytes = ScEvent.Parse(bytesEvent, 0);
            Assert.AreEqual(scEvent.SubscriptionId.Id, scEventFromBytes.SubscriptionId.Id);
            Assert.AreEqual(scEvent.ArcAddress.Segment, scEventFromBytes.ArcAddress.Segment);
            Assert.AreEqual(scEvent.ArcAddress.Offset, scEventFromBytes.ArcAddress.Offset);
            Assert.AreEqual(scEvent.ElementAddress.Segment, scEventFromBytes.ElementAddress.Segment);
            Assert.AreEqual(scEvent.ElementAddress.Offset, scEventFromBytes.ElementAddress.Offset);
        }
        #endregion

        #region SubscriptionId
        [TestMethod]
        [Timeout(3000)]
        public void TestSubscriptionId()
        {
            //new SubscriptionId
            SubscriptionId subscriptionId = new SubscriptionId(2);
            //get  bytes
            var bytes = subscriptionId.GetBytes();
            Assert.AreNotEqual(0, bytes.Length);
            byte[] bytesId = new byte[4] { 2, 0, 0, 0 };
            //new SubscriptionId from bytes
            SubscriptionId subscriptionIdFromBytes = SubscriptionId.Parse(bytesId, 0);
            Assert.AreEqual(subscriptionId.Id, subscriptionIdFromBytes.Id);
        }
        #endregion

        #region UnixDateTime
        [TestMethod]
        [Timeout(3000)]
        public void TestUnixDateTime()
        {
            UnixDateTime unixDateTime = new UnixDateTime(DateTime.Today);
            Assert.AreEqual(DateTime.Today, unixDateTime.ToDateTime());
        }
        #endregion

        //#region IteratorsChain
        //[TestMethod]
        //[Timeout(3000)]
        //public void TestIteratorsChain()
        //{
        //    KnowledgeBase knowledgeBase = new KnowledgeBase(SctpProtocol.TestServerIp, Ostis.Sctp.SctpProtocol.DefaultPortNumber);
        //    ConstructionTemplate initialIterator = new ConstructionTemplate(knowledgeBase.Commands.GetNodeAddress("nrel_system_identifier"), ElementType.ConstantCommonArc_c, ElementType.Link_a, ElementType.PositiveConstantPermanentAccessArc_c, knowledgeBase.Commands.GetNodeAddress("nrel_main_idtf"));
        //    ConstructionTemplate nextIterator = new ConstructionTemplate(knowledgeBase.Commands.GetNodeAddress("lang_ru"), ElementType.PositiveConstantPermanentAccessArc_c, new ScAddress(0, 0));
        //    IteratorsChainMember chainMember = new IteratorsChainMember(new Substitution(2, 2), nextIterator);
        //    IteratorsChain iterateChain = new IteratorsChain(initialIterator);
        //    iterateChain.ChainMembers.Add(chainMember);
        //}
        //#endregion

    }
}
