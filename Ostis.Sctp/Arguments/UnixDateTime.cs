using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time)
    /// </summary>
    public struct UnixDateTime : IArgument
    {
        private readonly byte[] bytes;
        private readonly uint length;
        private readonly Int64 value;

        /// <summary>
        /// Возвращает длину массива байт
        /// </summary>
        public uint Length
        { get { return length; } }

        /// <summary>
        /// Возвращает массив байт
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

        /// <summary>
        /// Инициализирует новую структуру <see cref="UnixDateTime"/> 
        /// </summary>
        /// <param name="datetime">Дата и время <see cref="System.DateTime"/> </param>
        public UnixDateTime(DateTime datetime)
        {
            length = 0;
            bytes = new byte[0];
#warning Вынести origin в константы
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = datetime - origin;
            value = (Int64) diff.TotalMilliseconds;

            bytes = BitConverter.GetBytes(value);
            length = (uint) bytes.Length;
        }

        /// <summary>
        /// Конвертирует дату и время Unix  в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="unixtime">Время DateTimeUnix</param>
        /// <returns></returns>
#warning static здесь не нужен - это обычный метод класса.
        public static DateTime ToDateTime(UnixDateTime unixtime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(unixtime.value);
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
