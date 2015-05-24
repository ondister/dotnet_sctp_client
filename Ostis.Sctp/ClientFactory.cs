
namespace Ostis.Sctp
{
    static internal class ClientFactory
    {
        public static IClient CreateClient(ClientType clienttype)
        {
            IClient _client = new SyncClient.SSctp_Client();
            switch (clienttype)
            {
                case ClientType.AsyncClient:
                    _client = new AsyncClient.ASctp_client();
                    break;
                case ClientType.SyncClient:
                    _client = new SyncClient.SSctp_Client();
                    break;
            }

            return _client;
        }
    }
}
