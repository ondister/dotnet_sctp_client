using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ostis.Sctp;

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

        }
    }
}
