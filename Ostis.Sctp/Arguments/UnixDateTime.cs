﻿using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Дата и время в форме Unix (http://en.wikipedia.org/wiki/Unix_time).
    /// </summary>
    public struct UnixDateTime : IArgument
    {
        private readonly long value;
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="dateTime">дата и время</param>
        public UnixDateTime(DateTime dateTime)
        {
            value = (long)(dateTime - Origin).TotalMilliseconds;
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
