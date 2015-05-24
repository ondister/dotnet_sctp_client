using System;

namespace sctp_client.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time)
    /// </summary>
    public struct DateTimeUNIX : IArgument
    {
        private byte[] _bytes;
        private uint _lenght;
        Int64 _value;

        /// <summary>
        /// Возвращает длину массива байт
        /// </summary>
        public uint Length
        {
            get { return _lenght; }
        }

        /// <summary>
        /// Возвращает массив байт
        /// </summary>
        public byte[] BytesStream
        {
            get { return _bytes; }
        }

        /// <summary>
        /// Инициализирует новую структуру <see cref="DateTimeUNIX"/> 
        /// </summary>
        /// <param name="datetime">Дата и время <see cref="System.DateTime"/> </param>
        public DateTimeUNIX(DateTime datetime)
        {
            _lenght = 0;
            _bytes = new byte[0];
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = datetime - origin;
            _value = (Int64)diff.TotalMilliseconds;

            _bytes = BitConverter.GetBytes(_value);
            _lenght = (uint)_bytes.Length;
        }

        /// <summary>
        ///Конвертирует дату и время Unix  в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="unixtime">Время DateTimeUnix</param>
        /// <returns></returns>
        public static DateTime ToDateTime(DateTimeUNIX unixtime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(unixtime._value);
        }

        /// <summary>
        /// Конвертирует дату и время Unix  в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="milliseconds">Время в миллисекундах</param>
        /// <returns></returns>
        public static DateTime ToDateTime(UInt64 milliseconds)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(milliseconds);
        }
    }
}
