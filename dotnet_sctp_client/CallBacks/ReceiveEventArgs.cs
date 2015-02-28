using System;
using sctp_client.AsyncClient;

namespace sctp_client.CallBacks
{
   internal delegate void ReceiveEventHandler(IClient sender,ReceiveEventArgs arg);

   internal class ReceiveEventArgs:EventArgs
    {
      
       public byte[] ReceivedBytes { get; set; }

       
    }
}
