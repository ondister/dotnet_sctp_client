using System;

namespace Ostis.Sctp
{
    /// <summary>
    /// Код выполнения команды.
    /// </summary>
    [Flags]
    public enum ReturnCode : ushort
    {
        /// <summary>
        /// Успешно.
        /// </summary>
        Successfull = 0x00,

        /// <summary>
        /// Не успешно.
        /// </summary>
        Failure = 0x01,

        /// <summary>
        /// Указанный sc-элемент не найден (неверный sc-адрес).
        /// </summary>
        AddressError = 0x02,
    }
}
