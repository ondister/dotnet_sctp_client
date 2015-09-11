using System;
using System.Threading;


using Ostis.Sctp;           // общее пространство имен, обязательно для подключения
using Ostis.Sctp.Arguments; // пространство имен аргументов команд
using Ostis.Sctp.Commands;  // пространство имен команд, отправляемых серверу
using Ostis.Sctp.Responses; // пространство имен ответов сервера
using Ostis.Sctp.Tools;
namespace Ostis.SctpDemo
{
	// ВНИМАНИЕ! Все примеры учебные, их правильное выполнение зависит от настроек вашей базы.
    // Если у вас нет элементов по указанным адресам - выйдут ошибки.
	// Все консультации и справки по скайпу ondister.
	// Сообщите, если хотите внести в этот код исправления или дополнения.
    internal class Demo
    {
        private SctpClient sctpClient;
        public void IterateConstructionDemo()
        {
            KnowledgeBase kbase = new KnowledgeBase("127.0.0.1", SctpProtocol.DefaultPortNumber);

            var initIterator = new ConstructionTemplate(kbase.GetNodeAddress("nrel_system_identifier"), ElementType.ConstantCommonArc, ElementType.Link);
            var iterChainMember = new IteratorsChainMember(new Substitution(1,2), new ConstructionTemplate(kbase.GetNodeAddress("nrel_main_idtf"), ElementType.PositiveConstantPermanentAccessArc, ScAddress.Unknown));
            IteratorsChain iterChain = new IteratorsChain(initIterator);
            iterChain.ChainMembers.Add(iterChainMember);

            this.Connect();
            var command = new IterateConstructionsCommand(iterChain);
            runAsyncTest(command);

            var response = (IterateConstructionsResponse)lastAsyncResponse;

            Console.WriteLine(response.Constructions.Count);

        }




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

