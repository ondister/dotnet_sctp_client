using System;
using System.Runtime.InteropServices;
using Ostis.Sctp.Arguments;
namespace Ostis.Sctp
{
    internal class Argument<T> : IArgument where T : struct
    {
        T _arg;
        public T Value { get { return _arg; } }

        public Argument(T arg)
        {
            _arg = arg;
        }

        public UInt32 Length
        {
            get
            {
                UInt32 _leight = 0;
                if (_arg is ElementType)
                {
                    byte[] bytes = BitConverter.GetBytes(Convert.ToUInt16(_arg));
                    _leight = (uint)bytes.Length;
                }
				else if (_arg is EventsType)
				{
					byte[] bytes =new byte[1]{Convert.ToByte(_arg)};
					_leight = (uint)bytes.Length;
				}
                else if (_arg is LinkContent)
                {
                    LinkContent tmpcont = (LinkContent)(object)_arg;
                    _leight = (uint)tmpcont.BytesStream.Length;
                }
                else if (_arg is Identifier)
                {
                    Identifier tmpcont = (Identifier)(object)_arg;
                    _leight = (uint)tmpcont.BytesStream.Length;
                }
                else if (_arg is ConstrTemplate)
                {
                    ConstrTemplate tmpcont = (ConstrTemplate)(object)_arg;
                    _leight = (uint)tmpcont.BytesStream.Length;
                }
                else if (_arg is DateTimeUNIX)
                {
                    DateTimeUNIX tmpcont = (DateTimeUNIX)(object)_arg;
                    _leight = (uint)tmpcont.BytesStream.Length;
                }
                else if (_arg is ScAddress)
                {
                    ScAddress tmpadr = (ScAddress)(object)_arg;
                    _leight = (uint)tmpadr.BytesStream.Length;
                }
                else
                {
                    _leight = Convert.ToUInt32(Marshal.SizeOf(_arg));
                }
                return _leight;

            }
        }


        public byte[] BytesStream
        {
            get
            {
                return GetBytes<T>(_arg);
            }
        }

        static unsafe byte[] GetBytes<To>(To obj) where To : struct
        {

            if (obj is ElementType)
            {
                return BitConverter.GetBytes(Convert.ToUInt16(obj));
            }
			else if (obj is EventsType)
			{
				return new byte[1]{Convert.ToByte(obj)};
			}
            else if (obj is LinkContent)
            {
                LinkContent tmpcont = (LinkContent)(object)obj;
                return tmpcont.BytesStream;
            }
            else if (obj is Identifier)
            {
                Identifier tmpcont = (Identifier)(object)obj;
                return tmpcont.BytesStream;
            }
            else if (obj is ConstrTemplate)
            {
                ConstrTemplate tmpcont = (ConstrTemplate)(object)obj;
                return tmpcont.BytesStream;
            }
            else if (obj is DateTimeUNIX)
            {
                DateTimeUNIX tmpcont = (DateTimeUNIX)(object)obj;
                return tmpcont.BytesStream;
            }
            else if (obj is ScAddress)
            {
                ScAddress tmpadr = (ScAddress)(object)obj;
                return tmpadr.BytesStream;
            }
            else
            {
                int size = Marshal.SizeOf(typeof(To));
                byte[] buffer = new byte[size];
                fixed (void* pointer = buffer)
                {
                    Marshal.StructureToPtr(obj, new IntPtr(pointer), false);

                }
                return buffer;
            }



        }
    }
}
