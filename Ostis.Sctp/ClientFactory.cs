using System;

namespace Ostis.Sctp
{
    static internal class ClientFactory
    {
        public static IClient CreateClient(ClientType type)
        {
            IClient client;
            switch (type)
            {
                case ClientType.AsyncClient:
                    client = new AsyncClient.ASctp_client();
                    break;
                case ClientType.SyncClient:
                    client = new SyncClient.SynchronousClient();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return client;
        }
    }
}
