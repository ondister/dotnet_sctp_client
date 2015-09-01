using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;


using Ostis.Sctp;           // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
using Ostis.Sctp.Commands;  // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses; // пространство имен ответов сервера

namespace Ostis.Tests
{
    [TestClass]
    public class CommandsTest
    {
        private  SctpClient sctpClient;
       
        #region CreateNode
        [TestMethod]
        public void TestCreateNodeSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            var command = new CreateNodeCommand(ElementType.ConstantNode);
            var response = (CreateNodeResponse)sctpClient.Send(command);

            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(response.Header.ReturnCode,ReturnCode.Successfull);
            Assert.AreNotEqual(response.CreatedNodeAddress.Offset, 0);
           
        }

        [TestMethod]
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


        #region DeleteNode
        [TestMethod]
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
            Assert.AreEqual(ReturnCode.Successfull,response.Header.ReturnCode);
            Assert.AreEqual(true, response.IsDeleted);
        }
        [TestMethod]
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
            var response= (DeleteElementResponse)lastAsyncResponse;
            Assert.AreEqual(command.Code, response.Header.CommandCode);
            Assert.AreEqual(ReturnCode.Successfull, response.Header.ReturnCode);
            Assert.AreEqual(true, response.IsDeleted);
        }
        #endregion











        #region Подключение
        private void Connect()
        {
            const string defaultAddress = "127.0.0.1";
           
            string serverAddress= defaultAddress;
           
            int serverPort= SctpProtocol.DefaultPortNumber;
            
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
