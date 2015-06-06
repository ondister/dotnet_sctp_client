using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Тип содержимого ссылки (не используется в связи с непонятками в сервере).
    /// </summary>
    [Flags]
    public enum LinkContentType : ushort
    {
        /// <summary>
        /// Тип неизвестен или не указан.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Число.
        /// </summary>
        Numeric = 0x01,

        /// <summary>
        /// Текст.
        /// </summary>
        Text = 0x02,

        /// <summary>
        /// Изображение.
        /// </summary>
        Image = 0x03,
    }
}
