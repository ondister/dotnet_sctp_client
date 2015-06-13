using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Адрес SC-элемента в памяти.
    /// </summary>
    public class ScAddress : IArgument
    {
        /// <summary>
        /// Сегмент.
        /// </summary>
        public ushort Segment
        { get { return segment; } }

        /// <summary>
        /// Смещение.
        /// </summary>
        public ushort Offset
        { get { return offset; } }

        private readonly ushort segment;
        private readonly ushort offset;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="segment">сегмент</param>
        /// <param name="offset">смещение</param>
        public ScAddress(ushort segment, ushort offset)
        {
            this.segment = segment;
            this.offset = offset;
        }

        /// <summary>
        /// Получает значение адреса из массива байт.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        /// <returns>SC-адрес</returns>
        public static ScAddress Parse(byte[] bytes, int offset)
        {
            return bytes.Length >= SctpProtocol.ScAddressLength + offset
                ? new ScAddress(
                    BitConverter.ToUInt16(bytes, sizeof(ushort) * 0 + offset),
                    BitConverter.ToUInt16(bytes, sizeof(ushort) * 1 + offset))
                : null;
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
		    return string.Format("segment: {0}, offset: {1}", segment, offset);
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new byte[4];
            Array.Copy(BitConverter.GetBytes(segment), bytes, 2);
            Array.Copy(BitConverter.GetBytes(offset), 0, bytes, 2, 2);
            return bytes;
        }

        #endregion
    }
}
