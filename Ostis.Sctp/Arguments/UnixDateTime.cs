using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time).
    /// </summary>
    public class UnixDateTime : IArgument
    {
        private readonly long value;
        
        /// <summary>
        /// Инициализирует новое время в формате UNIX.
        /// </summary>
        /// <param name="dateTime">дата и время</param>
        public UnixDateTime(DateTime dateTime)
        {
            value = (long)(dateTime - Origin).TotalSeconds;
        }

       

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время.
        /// </summary>
        /// <returns>System.DateTime</returns>
        public DateTime ToDateTime()
        {
            return ToDateTime(value);
        }

        /// <summary>
        /// Конвертирует дату и время Unix в дату и время <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="Seconds">Время в секундах</param>
        /// <returns></returns>
        public static DateTime ToDateTime(long milliseconds)
        {
            return Origin.AddSeconds(milliseconds);
        }

        public static UnixDateTime FromDateTime(DateTime dateTime)
        {
            var unixDateTime = new UnixDateTime(dateTime);
            return unixDateTime;
        }

        /// <summary>
        /// Начальная дата Unix в DateTime.
        /// </summary>
        public static readonly DateTime Origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(value);
        }

        #endregion
    }
}
