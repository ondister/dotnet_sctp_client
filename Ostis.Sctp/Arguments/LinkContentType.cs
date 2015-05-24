using System;

namespace sctp_client.Arguments
{
    /// <summary>
    /// Типы контента ссылки (не используется в связи с непонятками в сервере)
    /// </summary>
    [Flags]
   public enum LinkContentType:ushort
    {
        /// <summary>
        /// Тип неизвестен или не указан
        /// </summary>
        unknown=0x00,

        /// <summary>
        /// Число
        /// </summary>
        numeric=0x01,

        /// <summary>
        /// Текст
        /// </summary>
        text=0x02,

        /// <summary>
        /// Изображение
        /// </summary>
        image=0x03
    }
}
