using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
#region NameSpases
using Ostis.Sctp;               // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments;     // пространство имен аргументов команд
using Ostis.Sctp.Commands;      // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses;      // пространство имен ответов сервера
#endregion
namespace Ostis.Tests
{
    [TestClass]
    public class ConnectTest
    {
        [TestMethod]
        public void TestSocketConnect()
        {
            SctpClient sctpClient;
            const string defaultAddress = SctpProtocol.TestServerIp;
            string serverAddress = defaultAddress;
            int serverPort = SctpProtocol.DefaultPortNumber;
            sctpClient = new SctpClient(serverAddress, serverPort);

            sctpClient.Connect();
            Assert.AreEqual(true, sctpClient.IsConnected, "Подключение не удалось");
            Assert.AreEqual(SctpProtocol.TestServerIp, sctpClient.ServerEndPoint.Address.ToString());
            Assert.AreEqual(SctpProtocol.DefaultPortNumber, sctpClient.ServerEndPoint.Port);

        }




    }
}
