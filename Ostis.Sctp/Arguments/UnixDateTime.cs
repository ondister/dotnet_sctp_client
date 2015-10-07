using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix.
    ///  Дополнительная информация о формате Unixdate <a href="https://en.wikipedia.org/wiki/Unix_time"> здесь</a>
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="ConstructionTemplate"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="UnixDateTime" lang="C#" />
    /// </example>
    public class UnixDateTime : IArgument
    {
        private readonly ulong value;
        
        /// <summary>
        /// Инициализирует новое время в формате UNIX.
        /// </summary>
        /// <param name="dateTime">дата и время</param>
        public UnixDateTime(DateTime dateTime)
        {
            value = (ulong)(dateTime - Origin).TotalSeconds;
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
        /// <param name="seconds">Время в секундах</param>
        /// <returns></returns>
        public static DateTime ToDateTime(ulong seconds)
        {
            return Origin.AddSeconds(seconds);
        }

        /// <summary>
        /// Получает дату в формате UnixDate из формата <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="dateTime"><see cref="System.DateTime"/></param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает хэш-код значения
        /// </summary>
        /// <returns>Хэш-код значения</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
