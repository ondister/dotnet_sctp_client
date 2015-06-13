using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time).
    /// </summary>
    public struct UnixDateTime : IArgument
    {
        private readonly byte[] bytes;
        private readonly long value;
        
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
            bytes = new byte[0];
            TimeSpan diff = dateTime - Origin;
            value = (long) diff.TotalMilliseconds;
            bytes = BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время.
        /// </summary>
        /// <returns>System.DateTime</returns>
        public DateTime ToDateTime()
        {
#warning Разобраться с Long/ULong.
            return ToDateTime((ulong) value);
        }

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="milliseconds">Время в миллисекундах</param>
        /// <returns></returns>
        public static DateTime ToDateTime(ulong milliseconds)
        {
            return Origin.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// Начальная дата Unix.
        /// </summary>
        public static readonly DateTime Origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
    }
}
