using System;
using System.Runtime.InteropServices;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp
{
    internal class Argument<T> : IArgument
        where T : struct
    {
        private readonly T value;

        public T Value
        { get { return value; } }

        public Argument(T value)
        { this.value = value; }

        public UInt32 Length
        {
            get
            {
#warning Заменить эту цепочку IF-ов нормальным наследованием с полиморфизмом
                UInt32 length;
                if (value is ElementType)
                {
                    byte[] bytes = BitConverter.GetBytes(Convert.ToUInt16(value));
                    length = (uint) bytes.Length;
                }
                else if (value is EventsType)
                {
                    byte[] bytes = { Convert.ToByte(value) };
                    length = (uint) bytes.Length;
                }
                else if (value is LinkContent)
                {
                    var linkContent = (LinkContent) (object) value;
                    length = (uint) linkContent.BytesStream.Length;
                }
                else if (value is Identifier)
                {
                    var identifier = (Identifier) (object) value;
                    length = (uint) identifier.BytesStream.Length;
                }
                else if (value is ConstrTemplate)
                {
                    var constructorTemplate = (ConstrTemplate) (object) value;
                    length = (uint) constructorTemplate.BytesStream.Length;
                }
                else if (value is DateTimeUNIX)
                {
                    var dateTimeUnix = (DateTimeUNIX) (object) value;
                    length = (uint) dateTimeUnix.BytesStream.Length;
                }
                else if (value is ScAddress)
                {
                    var scAddress = (ScAddress) (object) value;
                    length = (uint) scAddress.BytesStream.Length;
                }
                else
                {
                    length = Convert.ToUInt32(Marshal.SizeOf(value));
                }
                return length;
            }
        }

        public byte[] BytesStream
        { get { return getBytes(value); } }

        private static unsafe byte[] getBytes(T obj)
        {
#warning Заменить эту цепочку IF-ов нормальным наследованием с полиморфизмом
#warning Проверить корреляцию этого кода с кодом предыдущего метода.
            if (obj is ElementType)
            {
                return BitConverter.GetBytes(Convert.ToUInt16(obj));
            }
            else if (obj is EventsType)
            {
                return new[] { Convert.ToByte(obj) };
            }
            else if (obj is LinkContent)
            {
                var linkContent = (LinkContent) (object) obj;
                return linkContent.BytesStream;
            }
            else if (obj is Identifier)
            {
                var identifier = (Identifier)(object)obj;
                return identifier.BytesStream;
            }
            else if (obj is ConstrTemplate)
            {
                var constructorTemplate = (ConstrTemplate)(object)obj;
                return constructorTemplate.BytesStream;
            }
            else if (obj is DateTimeUNIX)
            {
                var dateTimeUnix = (DateTimeUNIX)(object)obj;
                return dateTimeUnix.BytesStream;
            }
            else if (obj is ScAddress)
            {
                var scAddress = (ScAddress)(object)obj;
                return scAddress.BytesStream;
            }
            else
            {
                int size = Marshal.SizeOf(typeof (T));
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
