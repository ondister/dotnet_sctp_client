using System;

namespace Ostis.Sctp.CallBacks
{
    internal delegate void ReceiveEventHandler(IClient sender, ReceiveEventArgs arg);

    internal class ReceiveEventArgs : EventArgs
    {
        public byte[] ReceivedBytes
        { get; set; }
    }
}
