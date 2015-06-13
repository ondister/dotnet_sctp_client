using System;
using System.Runtime.InteropServices;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp
{
    /// <summary>
    /// Аргумент со значением.
    /// </summary>
    /// <typeparam name="T">тип значения</typeparam>
    public class Argument<T> : IArgument
        where T : struct
    {
        private readonly T value;

        /// <summary>
        /// Значение.
        /// </summary>
        public T Value
        { get { return value; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public Argument(T value)
        { this.value = value; }

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
                return linkContent.Bytes;
            }
            else if (obj is Identifier)
            {
                var identifier = (Identifier)(object)obj;
                return identifier.BytesStream;
            }
            else if (obj is ConstructionTemplate)
            {
                var constructorTemplate = (ConstructionTemplate)(object)obj;
                return constructorTemplate.BytesStream;
            }
            else if (obj is UnixDateTime)
            {
                var dateTimeUnix = (UnixDateTime)(object)obj;
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
