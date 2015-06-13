using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор SC-элемента.
    /// </summary>
    public struct Identifier : IArgument
    {
        private readonly byte[] bytes;

        /// <summary>
        /// Массив байт идентификатора.
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public Identifier(string value)
        {
            var bytesWithoutLength = SctpProtocol.TextEncoding.GetBytes(value);
            var bytesList = new List<byte>();
            bytesList.AddRange(BitConverter.GetBytes(bytesWithoutLength.Length));
            bytesList.AddRange(bytesWithoutLength);
            bytes = bytesList.ToArray();
        }

        /// <summary>
        /// Преобразование из строки.
        /// </summary>
        /// <param name="value">строковое значение</param>
        /// <returns>SC-идентификатор</returns>
        public static implicit operator Identifier(string value)
        {
            return new Identifier(value);
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            int length = BitConverter.ToInt32(bytes, 0);
            return SctpProtocol.TextEncoding.GetString(bytes, sizeof(int), length);
        }
    }
}
