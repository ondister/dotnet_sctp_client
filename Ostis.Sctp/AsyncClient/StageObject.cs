using System.IO;
using System.Net.Sockets;

namespace Ostis.Sctp.AsyncClient
{
    internal class StateObject
    {
        private Socket workSocket = null;

        public Socket WorkSocket
        {
            get { return workSocket; }
            set { workSocket = value; }
        }
        public const int BufferSize = 1024;



        private byte[] buffer = new byte[BufferSize];

        public byte[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }
        private MemoryStream stream = new MemoryStream();

        public MemoryStream Stream
        {
            get { return stream; }
            set { stream = value; }
        }
    }
}
