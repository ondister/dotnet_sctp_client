using System;

namespace Ostis.Sctp
{
    /// <summary>
    /// Тип содержимого ссылки.
    /// </summary>
    /// <remarks>
    /// Тип содержимого ссылки не имеет отношения к серверу. Это перечисление создано для удобства использования в реальных приложениях.
   ///</remarks>

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
