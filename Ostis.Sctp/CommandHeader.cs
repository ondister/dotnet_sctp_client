using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ostis.Sctp
{
    internal class CommandHeader
    {
        public byte Code
        { get; set; }

        public byte Flags
        { get; set; }

        public uint Id
        { get; set; }

        public uint ArgumentsSize
        { get; set; }

        public uint Length
        { get { return (uint)(Marshal.SizeOf(Code) + Marshal.SizeOf(Flags) + Marshal.SizeOf(Id) + Marshal.SizeOf(ArgumentsSize)); } }

        public byte[] BytesStream
        {
            get
            {
                var stream = new MemoryStream();
                if (Id != 0)
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                    {
                        writer.Write(Code);
                        writer.Write(Flags);
                        writer.Write(Id);
                        writer.Write(ArgumentsSize);
                    }
                }
                return stream.ToArray();
            }
        }
    }
}
