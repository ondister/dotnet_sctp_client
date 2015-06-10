using System;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    internal interface IClient : IDisposable
    {
        event ReceiveEventHandler Received;

        void Connect(string address, int port);

        void Send(byte[] bytes);

        bool Connected
        { get; }
    }
}
