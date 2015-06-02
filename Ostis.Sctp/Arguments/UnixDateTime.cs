using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time).
    /// </summary>
    public struct UnixDateTime : IArgument
    {
        private readonly byte[] bytes;
        private readonly uint length;
        private readonly Int64 value;

        /// <summary>
        /// Длина массива байт.
        /// </summary>
        public uint Length
        { get { return length; } }

        /// <summary>
        /// Массив байт.
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="dateTime">дата и время</param>
        public UnixDateTime(DateTime dateTime)
        {
            length = 0;
            bytes = new byte[0];
            TimeSpan diff = dateTime - Origin;
            value = (Int64) diff.TotalMilliseconds;
            bytes = BitConverter.GetBytes(value);
            length = (uint) bytes.Length;
        }

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время.
        /// </summary>
        /// <returns>System.DateTime</returns>
        public DateTime ToDateTime()
        {
#warning Разобраться с Long/ULong.
//мда напутал немного, надо закурить вики
            return ToDateTime((ulong) value);
        }

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="milliseconds">Время в миллисекундах</param>
        /// <returns></returns>
        public static DateTime ToDateTime(UInt64 milliseconds)
        {
            return Origin.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// Начальная дата Unix.
        /// </summary>
        public static readonly DateTime Origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }
}
