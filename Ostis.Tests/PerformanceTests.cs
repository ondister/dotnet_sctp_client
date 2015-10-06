using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#region NameSpases
using Ostis.Sctp;               // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments;     // пространство имен аргументов команд
using Ostis.Sctp.Commands;      // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses;
using System.Threading;
using Ostis.Sctp.Tools;      // пространство имен ответов сервера
#endregion

namespace Ostis.Tests
{
    [TestClass]
   public class PerformanceTests
    {
        private SctpClient sctpClient;

        #region CreateNodesPerformance
        [TestMethod]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestPerfCreateNodesSync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            for (int count = 0; count < 1000000; count++)
            {
                var command = new CreateNodeCommand(ElementType.ConstantNode_c);
                var response = (CreateNodeResponse)sctpClient.Send(command);
                Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull,"Убедитесь, что параметр максимальное количество сегментов в файле конфигурации сервера не менее 20");
                
            }

        }

        [TestMethod]
        [TestProperty("Синхронность", "Асинхронный")]
        public void TestPerfCreateNodesASync()
        {
            this.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected);
            for (int count = 0; count < 1000000; count++)
            {
                var command = new CreateNodeCommand(ElementType.ConstantNode_c);
                runAsyncTest(command);
                var response = (CreateNodeResponse)lastAsyncResponse;
                Assert.AreEqual(response.Header.ReturnCode, ReturnCode.Successfull, "Убедитесь, что параметр максимальное количество сегментов в файле конфигурации сервера не менее 20");
               
            }

        }

        [TestMethod]
        [TestProperty("Синхронность", "Синхронный")]
        public void TestPerfCreateNodesTools()
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase("127.0.0.1", Ostis.Sctp.SctpProtocol.DefaultPortNumber);
            for (int count = 0; count < 100000; count++)
            {
                knowledgeBase.Nodes.Add(ElementType.Node_a, "idtf_" + count.ToString());

            }

        }

        #endregion

        #region Connect
        private void Connect()
        {
            const string defaultAddress = "127.0.0.1";
            string serverAddress = defaultAddress;
            int serverPort = SctpProtocol.DefaultPortNumber;
            sctpClient = new SctpClient(serverAddress, serverPort);
            //подписываемся на событие, если планируем использовать асинхронный клиент
            sctpClient.ResponseReceived += asyncHandler;
            //подключаемся
            sctpClient.Connect();
        }
        #endregion

        #region AsyncHandlers
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
