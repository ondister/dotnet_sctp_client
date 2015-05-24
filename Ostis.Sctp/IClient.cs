using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sctp_client.CallBacks;

namespace sctp_client
{
    internal interface IClient
    {
        event ReceiveEventHandler Received;
        void Connect(string address, int port);
        void Disconnect();
        void SendBytes(byte[] bytestosend);
        bool Connected { get; }
    }
}
