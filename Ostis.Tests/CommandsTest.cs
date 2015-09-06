using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;


using Ostis.Sctp;           // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
using Ostis.Sctp.Commands;  // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses;
using System.Collections.Generic; // пространство имен ответов сервера

namespace Ostis.Tests
{
    [TestClass]
    public class CommandsTest
    {
        private SctpClient sctpClient;

        #region CreateNode
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestCreateNodeSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new CreateNodeCommand(ElementType.ConstantNode);
            var response = (CreateNodeResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.CreatedNodeAddress.Offset, 0);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestCreateNodeASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(command);
            var response = (CreateNodeResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.CreatedNodeAddress.Offset, 0);

        }

        #endregion


        #region CreateArc
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestCreateArcSync()
        {
            //create the node1
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode1 = (CreateNodeResponse)sctpClient.Send(commandCreateNode1);
            //create the node2
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode2 = (CreateNodeResponse)sctpClient.Send(commandCreateNode2);
            //Create the Arc
            var command = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            var response = (CreateArcResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreNotEqual(response.CreatedArcAddress.Offset, 0);
        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestCreateArcASync()
        {
            //create the node1
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode1);
            var responseCreateNode1 = (CreateNodeResponse)lastAsyncResponse;
            //create the node2
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode2);
            var responseCreateNode2 = (CreateNodeResponse)lastAsyncResponse;
            //Create the Arc
            var command = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            runAsyncTest(command);
            var response = (CreateArcResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreNotEqual(response.CreatedArcAddress.Offset, 0);
        }

        #endregion


        #region CreateLink
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestCreateLinkSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new CreateLinkCommand();
            var response = (CreateLinkResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.CreatedLinkAddress.Offset, 0);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestCreateLinkASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new CreateLinkCommand();
            runAsyncTest(command);
            var response = (CreateLinkResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.CreatedLinkAddress.Offset, 0);

        }

        #endregion


        #region DeleteNode
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestDeleteNodeSync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreate = (CreateNodeResponse)sctpClient.Send(commandCreate);
            //delete the node
            var command = new DeleteElementCommand(responseCreate.CreatedNodeAddress);
            var response = (DeleteElementResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(true, response.IsDeleted);
        }
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestDeleteNodeASync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreate);
            var responseCreate = (CreateNodeResponse)lastAsyncResponse;
            //delete the node
            var command = new DeleteElementCommand(responseCreate.CreatedNodeAddress);
            runAsyncTest(command);
            var response = (DeleteElementResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(true, response.IsDeleted);
        }
        #endregion


        #region CheckElement
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestCheckElementSync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreate = (CreateNodeResponse)sctpClient.Send(commandCreate);
            //check the node
            var command = new CheckElementCommand(responseCreate.CreatedNodeAddress);
            var response = (CheckElementResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(true, response.ElementExists);
        }
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestCheckElementASync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreate);
            var responseCreate = (CreateNodeResponse)lastAsyncResponse;
            //check the node
            var command = new CheckElementCommand(responseCreate.CreatedNodeAddress);
            runAsyncTest(command);
            var response = (CheckElementResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(true, response.ElementExists);
        }



        #endregion


        #region FindElement
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestFindElementSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //looking for system identifier
            var command = new FindElementCommand(new Identifier("nrel_system_identifier"));
            var response = (FindElementResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.FoundAddress.Offset, 0);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestFindElementASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //looking for system identifier
            var command = new FindElementCommand(new Identifier("nrel_system_identifier"));
            runAsyncTest(command);
            var response = (FindElementResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.FoundAddress.Offset, 0);

        }

        #endregion


        #region FindLinks
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestFindLinksSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //looking for system identifier
            var command = new FindLinksCommand(new LinkContent("nrel_system_identifier"));
            var response = (FindLinksResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.Addresses.Count, 0);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestFindLinksASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //looking for system identifier
            var command = new FindLinksCommand(new LinkContent("nrel_system_identifier"));
            runAsyncTest(command);
            var response = (FindLinksResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.Addresses.Count, 0);

        }

        #endregion


        #region GetArcElements
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestGetArcElementsSync()
        {
            //create the node1
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode1 = (CreateNodeResponse)sctpClient.Send(commandCreateNode1);
            //create the node2
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode2 = (CreateNodeResponse)sctpClient.Send(commandCreateNode2);
            //Create the Arc
            var commandCreateArc = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            var responseCreateArc = (CreateArcResponse)sctpClient.Send(commandCreateArc);
            //Get arc elements
            var command = new GetArcElementsCommand(responseCreateArc.CreatedArcAddress);
            var response = (GetArcElementsResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(responseCreateNode1.CreatedNodeAddress.Offset, response.BeginElementAddress.Offset);
            Assert.AreEqual(responseCreateNode2.CreatedNodeAddress.Offset, response.EndElementAddress.Offset);
        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestGetArcElementsASync()
        {
            //create the node1
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode1);
            var responseCreateNode1 = (CreateNodeResponse)lastAsyncResponse;
            //create the node2
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode2);
            var responseCreateNode2 = (CreateNodeResponse)lastAsyncResponse;
            //Create the Arc
            var commandCreateArc = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            runAsyncTest(commandCreateArc);
            var responseCreateArc = (CreateArcResponse)lastAsyncResponse;
            //Get arc elements
            var command = new GetArcElementsCommand(responseCreateArc.CreatedArcAddress);
            runAsyncTest(command);
            var response = (GetArcElementsResponse)lastAsyncResponse;

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(responseCreateNode1.CreatedNodeAddress.Offset, response.BeginElementAddress.Offset);
            Assert.AreEqual(responseCreateNode2.CreatedNodeAddress.Offset, response.EndElementAddress.Offset);

        }

        #endregion


        #region GetElementType
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestGetElementTypeSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the node
            var commandCreateNode = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode = (CreateNodeResponse)sctpClient.Send(commandCreateNode);
            //check the element type
            var command = new GetElementTypeCommand(responseCreateNode.CreatedNodeAddress);
            var response = (GetElementTypeResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreEqual(response.ElementType, ElementType.ConstantNode);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestGetElementTypeASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the node
            var commandCreateNode = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode);
            var responseCreateNode = (CreateNodeResponse)lastAsyncResponse;
            //check the element type
            var command = new GetElementTypeCommand(responseCreateNode.CreatedNodeAddress);
            runAsyncTest(command);
            var response = (GetElementTypeResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreEqual(response.ElementType, ElementType.ConstantNode);

        }
        #endregion


        #region SetLinkContent
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestSetLinkContentSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the link
            var commandCreateLink = new CreateLinkCommand();
            var responseCreateLink = (CreateLinkResponse)sctpClient.Send(commandCreateLink);
            //set the content
            var command = new SetLinkContentCommand(responseCreateLink.CreatedLinkAddress, new LinkContent("test и тест"));
            var response = (SetLinkContentResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.IsTrue(response.ContentIsSet);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestSetLinkContentASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the link
            var commandCreateLink = new CreateLinkCommand();
            runAsyncTest(commandCreateLink);
            var responseCreateLink = (CreateLinkResponse)lastAsyncResponse;
            //set the content
            var command = new SetLinkContentCommand(responseCreateLink.CreatedLinkAddress, new LinkContent("test и тест"));
            runAsyncTest(command);
            var response = (SetLinkContentResponse)lastAsyncResponse;

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.IsTrue(response.ContentIsSet);

        }

        #endregion


        #region GetLinkContent
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestGetLinkContentSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the link
            var commandCreateLink = new CreateLinkCommand();
            var responseCreateLink = (CreateLinkResponse)sctpClient.Send(commandCreateLink);
            //set the content
            var commandSetContent = new SetLinkContentCommand(responseCreateLink.CreatedLinkAddress, new LinkContent("test и тест"));
            var responseSetContent = (SetLinkContentResponse)sctpClient.Send(commandSetContent);
            //get the content
            var command = new GetLinkContentCommand(responseCreateLink.CreatedLinkAddress);
            var response = (GetLinkContentResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreEqual(LinkContent.ToString(commandSetContent.Content.Data), LinkContent.ToString(response.LinkContent));

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestGetLinkContentASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            //create the link
            var commandCreateLink = new CreateLinkCommand();
            runAsyncTest(commandCreateLink);
            var responseCreateLink = (CreateLinkResponse)lastAsyncResponse;
            //set the content
            var commandSetContent = new SetLinkContentCommand(responseCreateLink.CreatedLinkAddress, new LinkContent("test и тест"));
            runAsyncTest(commandSetContent);
            var responseSetContent = (SetLinkContentResponse)lastAsyncResponse;
            //get the content
            var command = new GetLinkContentCommand(responseCreateLink.CreatedLinkAddress);
            runAsyncTest(command);
            var response = (GetLinkContentResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreEqual(LinkContent.ToString(commandSetContent.Content.Data), LinkContent.ToString(response.LinkContent));

        }

        #endregion


        #region SetSystemID
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestSetSystemIDSync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreate = (CreateNodeResponse)sctpClient.Send(commandCreate);
            //Set the ID
            var command = new SetSystemIdCommand(responseCreate.CreatedNodeAddress, new Identifier("new_sys_id"));
            var response = (SetSystemIdResponse)sctpClient.Send(command);
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode,"Возможно тест запускается второй раз за сессию сервера и системный идентификатор дублируется");
            Assert.IsTrue(response.IsSuccesfull);
            //Get the id
            var commandFindElement = new FindElementCommand(new Identifier("new_sys_id"));
            var responseFindElement = (FindElementResponse)sctpClient.Send(commandFindElement);
            Assert.AreEqual(responseCreate.CreatedNodeAddress.Offset, responseFindElement.FoundAddress.Offset);
        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestSetSystemIDASync()
        {
            //create the node
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreate = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreate);
            var responseCreate = (CreateNodeResponse)lastAsyncResponse;
            //Set the ID
            var command = new SetSystemIdCommand(responseCreate.CreatedNodeAddress, new Identifier("new_sys_id_async"));
            runAsyncTest(command);
            var response = (SetSystemIdResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode, "Возможно тест запускается второй раз за сессию сервера и системный идентификатор дублируется");
            Assert.IsTrue(response.IsSuccesfull);
            //Get the id
            var commandFindElement = new FindElementCommand(new Identifier("new_sys_id_async"));
            runAsyncTest(commandFindElement);
            var responseFindElement = (FindElementResponse)lastAsyncResponse;
            Assert.AreEqual(responseCreate.CreatedNodeAddress.Offset, responseFindElement.FoundAddress.Offset);
        }

        #endregion


        #region GetProtocolVersion
        [TestMethod]
        [Timeout(3000)]
     // [TestProperty("Синхронность", "Синхронный")]
        [TestProperty("Реализация на сервере", "Не реализована")]
        public void TestGetProtocolVersionSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new GetProtocolVersionCommand();
            var response = (GetProtocolVersionResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.IsInstanceOfType(response.ProtocolVersion, typeof(int));

        }

        [TestMethod]
        [Timeout(3000)]
       // [TestProperty("Синхронность", "Асинхронный")]
        [TestProperty("Реализация на сервере", "Не реализована")]
        public void TestGetProtocolVersionASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new GetProtocolVersionCommand();
            runAsyncTest(command);
            var response = (GetProtocolVersionResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.IsInstanceOfType(response.ProtocolVersion, typeof(int));

        }
        #endregion


        #region GetStatistics
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestGetStatisticsSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var startUnixDateTime = UnixDateTime.FromDateTime(DateTime.MinValue);
            var endUnixDateTime = UnixDateTime.FromDateTime(DateTime.Now);
            var command = new GetStatisticsCommand(startUnixDateTime, endUnixDateTime);
            var response = (GetStatisticsResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.TimeChecksCount, 0);
            var statisticsData = response.StatisticsDataList[0];
            Assert.IsNotNull(statisticsData.ArcCount);
            Assert.IsNotNull(statisticsData.CommandErrorsCount);
            Assert.IsNotNull(statisticsData.CommandsCount);
            Assert.IsNotNull(statisticsData.ConnectionsCount);
            Assert.IsNotNull(statisticsData.EmptyCount);
            Assert.IsNotNull(statisticsData.LinksCount);
            Assert.IsNotNull(statisticsData.LiveArcCount);
            Assert.IsNotNull(statisticsData.LiveLinkCount);
            Assert.IsNotNull(statisticsData.LiveNodeCount);
            Assert.IsNotNull(statisticsData.NodeCount);
            Assert.IsNotNull(statisticsData.Time);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestGetStatisticsASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var startUnixDateTime = UnixDateTime.FromDateTime(DateTime.MinValue);
            var endUnixDateTime = UnixDateTime.FromDateTime(DateTime.Now);
            var command = new GetStatisticsCommand(startUnixDateTime, endUnixDateTime);
            runAsyncTest(command);
            var response = (GetStatisticsResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull);
            Assert.AreNotEqual(response.TimeChecksCount, 0);
            var statisticsData = response.StatisticsDataList[0];
            Assert.IsNotNull(statisticsData.ArcCount);
            Assert.IsNotNull(statisticsData.CommandErrorsCount);
            Assert.IsNotNull(statisticsData.CommandsCount);
            Assert.IsNotNull(statisticsData.ConnectionsCount);
            Assert.IsNotNull(statisticsData.EmptyCount);
            Assert.IsNotNull(statisticsData.LinksCount);
            Assert.IsNotNull(statisticsData.LiveArcCount);
            Assert.IsNotNull(statisticsData.LiveLinkCount);
            Assert.IsNotNull(statisticsData.LiveNodeCount);
            Assert.IsNotNull(statisticsData.NodeCount);
            Assert.IsNotNull(statisticsData.Time);

        }

        #endregion


        #region IterateElements




        [TestMethod]
        [Timeout(10000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestIterateElementsSync()
        {
            this.Connect();
            Assert.IsTrue(sctpClient.IsConnected);
            //ищем адрес идентификатора узел
            var commandFindByID = new FindElementCommand(new Identifier("nrel_system_identifier"));

            var responseFindByID = (FindElementResponse)sctpClient.Send(commandFindByID);
            Assert.AreEqual(ReturnCode.Successfull, responseFindByID.Header.ReturnCode);
            if (responseFindByID.Header.ReturnCode == ReturnCode.Successfull)
            {

                //ищем адреса всех дуг, в которые входит идентификатор
                var template = new ConstructionTemplate(responseFindByID.FoundAddress, ElementType.AccessArc, ElementType.CommonArc);
                var commandIterate = new IterateElementsCommand(template);
                var responseIterate = (IterateElementsResponse)sctpClient.Send(commandIterate);
                Assert.AreNotEqual(responseIterate.Constructions.Count, 0);
                foreach (var construction in responseIterate.Constructions)
                {
                    //ищем узел, из которого отходит дуга
                    var commandGetNode = new GetArcElementsCommand(construction[2]);
                    var responseGetNode = (GetArcElementsResponse)sctpClient.Send(commandGetNode);
                    //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
                    var commandGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
                    var respondeGetLinkContent = (GetLinkContentResponse)sctpClient.Send(commandGetLinkContent);
                    //теперь смотрим, есть ли у него хотя бы один основной идентификатор 
                    //для этого смотрим адрес идентификатора
                    var cmdFindBydId = new FindElementCommand(new Identifier("nrel_main_idtf"));
                    var rspFindById = (FindElementResponse)sctpClient.Send(cmdFindBydId);

                    //и итерируем 
                    var itertemplate = new ConstructionTemplate(responseGetNode.BeginElementAddress, ElementType.CommonArc, ElementType.Link, ElementType.AccessArc, rspFindById.FoundAddress);
                    var cmdIterate = new IterateElementsCommand(itertemplate);
                    var rspIterate = (IterateElementsResponse)sctpClient.Send(cmdIterate);

                }
            }
        }


        [TestMethod]
        [Timeout(10000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestIterateElementsASync()
        {
            this.Connect();
            Assert.IsTrue(sctpClient.IsConnected);
            //ищем адрес идентификатора узел
            var commandFindByID = new FindElementCommand(new Identifier("nrel_system_identifier"));
            runAsyncTest(commandFindByID);
            var responseFindByID = (FindElementResponse)lastAsyncResponse;
            Assert.AreEqual(ReturnCode.Successfull, responseFindByID.Header.ReturnCode);
            if (responseFindByID.Header.ReturnCode == ReturnCode.Successfull)
            {

                //ищем адреса всех дуг, в которые входит идентификатор
                var template = new ConstructionTemplate(responseFindByID.FoundAddress, ElementType.AccessArc, ElementType.CommonArc);
                var commandIterate = new IterateElementsCommand(template);
                runAsyncTest(commandIterate);
                var responseIterate = (IterateElementsResponse)lastAsyncResponse;
                Assert.AreNotEqual(responseIterate.Constructions.Count, 0);
                foreach (var construction in responseIterate.Constructions)
                {
                    //ищем узел, из которого отходит дуга
                    var commandGetNode = new GetArcElementsCommand(construction[2]);
                    runAsyncTest(commandGetNode);
                    var responseGetNode = (GetArcElementsResponse)lastAsyncResponse;
                    //искомый узел будет responseGetNode.BeginElementAddress, а ссылка  responseGetNode.EndElementAddress
                    var commandGetLinkContent = new GetLinkContentCommand(responseGetNode.EndElementAddress);
                    runAsyncTest(commandGetLinkContent);
                    var respondeGetLinkContent = (GetLinkContentResponse)lastAsyncResponse;
                    //теперь смотрим, есть ли у него хотя бы один основной идентификатор 
                    //для этого смотрим адрес идентификатора
                    var cmdFindBydId = new FindElementCommand(new Identifier("nrel_main_idtf"));
                    runAsyncTest(cmdFindBydId);
                    var rspFindById = (FindElementResponse)lastAsyncResponse;

                    //и итерируем 
                    var itertemplate = new ConstructionTemplate(responseGetNode.BeginElementAddress, ElementType.CommonArc, ElementType.Link, ElementType.AccessArc, rspFindById.FoundAddress);
                    var cmdIterate = new IterateElementsCommand(itertemplate);
                    runAsyncTest(cmdIterate);
                    var rspIterate = (IterateElementsResponse)lastAsyncResponse;

                }
            }
        }

        #endregion


        #region SubScriptions
        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestSubscriptionsSync()
        {
            this.Connect();
            Assert.IsTrue(sctpClient.IsConnected);
            //create the node1 
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode1 = (CreateNodeResponse)sctpClient.Send(commandCreateNode1);

            // create the node2
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            var responseCreateNode2 = (CreateNodeResponse)sctpClient.Send(commandCreateNode2);

            //subscriptionsNode1
            var commandCreateSubscriptionCreate1 = new CreateSubscriptionCommand(EventType.AddOutArc, responseCreateNode1.CreatedNodeAddress);
            var responseCreateSubscriptionCreate1 = (CreateSubscriptionResponse)sctpClient.Send(commandCreateSubscriptionCreate1);
            SubscriptionId SubscriptionCreate1 = responseCreateSubscriptionCreate1.SubscriptionId;
            // subscriptionsNode2
            var commandCreateSubscriptionCreate2 = new CreateSubscriptionCommand(EventType.AddInArc, responseCreateNode2.CreatedNodeAddress);
            var responseCreateSubscriptionCreate2 = (CreateSubscriptionResponse)sctpClient.Send(commandCreateSubscriptionCreate2);
            SubscriptionId SubscriptionCreate2 = responseCreateSubscriptionCreate2.SubscriptionId;


            //Create the Arc
            var commandCreateArc = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            var responseCreateArc = (CreateArcResponse)sctpClient.Send(commandCreateArc);
            Assert.AreEqual(commandCreateArc.Code, responseCreateArc.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, responseCreateArc.Header.ReturnCode);
            Assert.AreNotEqual(responseCreateArc.CreatedArcAddress.Offset, 0);

            //subscriptionsArc
            var commandCreateSubscriptionDel = new CreateSubscriptionCommand(EventType.DeleteElement, responseCreateArc.CreatedArcAddress);
            var responseCreateSubscriptionDel = (CreateSubscriptionResponse)sctpClient.Send(commandCreateSubscriptionDel);
            SubscriptionId SubscriptionDel = responseCreateSubscriptionDel.SubscriptionId;
            //delete the arc
            var commandDelete = new DeleteElementCommand(responseCreateArc.CreatedArcAddress);
            var reaponseDelete = (DeleteElementResponse)sctpClient.Send(commandDelete);


            //emit any events
            Thread.Sleep(1000);
            var commandEmit = new EmitEventsCommand();
            var responseEmit = (EmitEventsResponse)sctpClient.Send(commandEmit);
            List<ScEvent> eventList = responseEmit.ScEvents;
            Assert.AreEqual(3, eventList.Count);

            //find subscriptions
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionCreate1.Id));
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionCreate2.Id));
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionDel.Id));

            Assert.IsTrue(eventList.Exists(ev => ev.ElementAddress.Offset == responseCreateNode1.CreatedNodeAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ArcAddress.Offset == responseCreateArc.CreatedArcAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ElementAddress.Offset == responseCreateNode2.CreatedNodeAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ArcAddress.Offset == responseCreateArc.CreatedArcAddress.Offset));

            //delete subscription
            var commandDeleteSubscriptionDel = new DeleteSubscriptionCommand(SubscriptionDel);
            var responseDeleteSubscriptionDel = (DeleteSubscriptionResponse)sctpClient.Send(commandDeleteSubscriptionDel);

            Assert.AreEqual(ReturnCode.Successfull, responseDeleteSubscriptionDel.Header.ReturnCode);
            Assert.AreEqual(responseCreateSubscriptionDel.SubscriptionId.Id, responseDeleteSubscriptionDel.SubscriptionId.Id);

        }

        [TestMethod]
        [Timeout(3000)]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestSubscriptionsASync()
        {
            this.Connect();
            Assert.IsTrue(sctpClient.IsConnected);

            //create the node1
            var commandCreateNode1 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode1);
            var responseCreateNode1 = (CreateNodeResponse)lastAsyncResponse;

            //create the node2
            Assert.AreEqual(true, sctpClient.IsConnected);
            var commandCreateNode2 = new CreateNodeCommand(ElementType.ConstantNode);
            runAsyncTest(commandCreateNode2);
            var responseCreateNode2 = (CreateNodeResponse)lastAsyncResponse;

            //subscriptionsNode1
            var commandCreateSubscriptionCreate1 = new CreateSubscriptionCommand(EventType.AddOutArc, responseCreateNode1.CreatedNodeAddress);
            runAsyncTest(commandCreateSubscriptionCreate1);
            var responseCreateSubscriptionCreate1 = (CreateSubscriptionResponse)lastAsyncResponse;
            SubscriptionId SubscriptionCreate1 = responseCreateSubscriptionCreate1.SubscriptionId;
            Assert.AreEqual(ReturnCode.Successfull, responseCreateSubscriptionCreate1.Header.ReturnCode);

            //subscriptionsNode2
            var commandCreateSubscriptionCreate2 = new CreateSubscriptionCommand(EventType.AddInArc, responseCreateNode2.CreatedNodeAddress);
            runAsyncTest(commandCreateSubscriptionCreate2);
            var responseCreateSubscriptionCreate2 = (CreateSubscriptionResponse)lastAsyncResponse;
            SubscriptionId SubscriptionCreate2 = responseCreateSubscriptionCreate2.SubscriptionId;
            Assert.AreEqual(ReturnCode.Successfull, responseCreateSubscriptionCreate2.Header.ReturnCode);

            //Create the Arc
            var commandCreateArc = new CreateArcCommand(ElementType.ConstantCommonArc, responseCreateNode1.CreatedNodeAddress, responseCreateNode2.CreatedNodeAddress);
            runAsyncTest(commandCreateArc);
            var responseCreateArc = (CreateArcResponse)lastAsyncResponse;
            Assert.AreEqual(commandCreateArc.Code, responseCreateArc.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, responseCreateArc.Header.ReturnCode);
            Assert.AreNotEqual(responseCreateArc.CreatedArcAddress.Offset, 0);

            //subscriptionsArc
            var commandCreateSubscriptionDel = new CreateSubscriptionCommand(EventType.DeleteElement, responseCreateArc.CreatedArcAddress);
            runAsyncTest(commandCreateSubscriptionDel);
            var responseCreateSubscriptionDel = (CreateSubscriptionResponse)lastAsyncResponse;
            SubscriptionId SubscriptionDel = responseCreateSubscriptionDel.SubscriptionId;
            Assert.AreEqual(ReturnCode.Successfull, responseCreateSubscriptionDel.Header.ReturnCode);

            //delete the arc
            var commandDelete = new DeleteElementCommand(responseCreateArc.CreatedArcAddress);
            runAsyncTest(commandDelete);
            var reaponseDelete = (DeleteElementResponse)lastAsyncResponse;

            //emit any events
            Thread.Sleep(1000);
            var commandEmit = new EmitEventsCommand();
            var responseEmit = (EmitEventsResponse)sctpClient.Send(commandEmit);
            List<ScEvent> eventList = responseEmit.ScEvents;
            Assert.AreEqual(3, eventList.Count);

            //find subscriptions
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionCreate1.Id));
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionCreate2.Id));
            Assert.IsTrue(eventList.Exists(ev => ev.Id.Id == SubscriptionDel.Id));

            Assert.IsTrue(eventList.Exists(ev => ev.ElementAddress.Offset == responseCreateNode1.CreatedNodeAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ArcAddress.Offset == responseCreateArc.CreatedArcAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ElementAddress.Offset == responseCreateNode2.CreatedNodeAddress.Offset));
            Assert.IsTrue(eventList.Exists(ev => ev.ArcAddress.Offset == responseCreateArc.CreatedArcAddress.Offset));
           
            //delete subscription
            var commandDeleteSubscriptionDel = new DeleteSubscriptionCommand(SubscriptionDel);
            var responseDeleteSubscriptionDel = (DeleteSubscriptionResponse)sctpClient.Send(commandDeleteSubscriptionDel);

            Assert.AreEqual(ReturnCode.Successfull, responseDeleteSubscriptionDel.Header.ReturnCode);
            Assert.AreEqual(responseCreateSubscriptionDel.SubscriptionId.Id, responseDeleteSubscriptionDel.SubscriptionId.Id);

        }
        #endregion





        #region Подключение
        private void Connect()
        {
            const string defaultAddress = "127.0.0.1";

            string serverAddress = defaultAddress;

            int serverPort = SctpProtocol.DefaultPortNumber;

            sctpClient = new SctpClient(serverAddress, serverPort);
            sctpClient.ResponseReceived += asyncHandler;

            sctpClient.Connect();

        }


        private void runAsyncTest(Command command)
        {
            lastAsyncResponse = null;
            synchronizer.Reset();
            sctpClient.SendAsync(command);
            synchronizer.WaitOne();
        }

        private void asyncHandler(Command command, Response response)
        {
            lastAsyncResponse = response;
            synchronizer.Set();
        }

        private readonly ManualResetEvent synchronizer = new ManualResetEvent(false);
        private Response lastAsyncResponse;
        #endregion
    }
}
