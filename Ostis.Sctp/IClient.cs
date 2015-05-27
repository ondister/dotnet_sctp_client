using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    internal interface IClient
    {
        event ReceiveEventHandler Received;

        void Connect(string address, int port);

        void Disconnect();

        void Send(byte[] bytes);

        bool Connected
        { get; }
    }
}
