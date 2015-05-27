using System.IO;
using System.Net.Sockets;

namespace Ostis.Sctp.AsyncClient
{
    internal class StateObject
    {
        public const int BufferSize = 1024;

        public Socket WorkSocket
        { get; set; }
        
        public byte[] Buffer
        { get; set; }

        public MemoryStream Stream
        { get; set; }

        public StateObject()
        {
            Stream = new MemoryStream();
            Buffer = new byte[BufferSize];
            WorkSocket = null;
        }
    }
}
