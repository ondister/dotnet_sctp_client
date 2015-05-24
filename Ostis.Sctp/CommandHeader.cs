using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ostis.Sctp
{
    internal class CommandHeader
    {
        private byte _code;
        private byte _flags;
        private UInt32 _id;
        private UInt32 _argsize;

        public byte Code
        {
            get { return _code; }
            set { _code = value; }
        }
       
        public byte Flags
        {
            get { return _flags; }
            set { _flags = value; }
        }
       
        public UInt32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
       
        public UInt32 ArgSize
        {
            get { return _argsize; }
            set { _argsize = value; }
        }

        public UInt32 Length
        {
            get 
            {
                return Convert.ToUInt32(Marshal.SizeOf(_code) + 
                                        Marshal.SizeOf(_flags) + 
                                        Marshal.SizeOf(_id) + 
                                        Marshal.SizeOf(_argsize));
            }
        }

        public byte[] BytesStream
        {
            get
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
             
                if (_id != 0)
                {
                    writer.Write((byte)_code);
                    writer.Write((byte)_flags);
                    writer.Write(_id);
                    writer.Write(_argsize);
                }
               writer.Close();


               return stream.ToArray();
            }
        }

    }
}
