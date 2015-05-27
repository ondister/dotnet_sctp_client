using System;

namespace Ostis.Sctp.CallBacks
{
    /// <summary>
    /// Коды выполнения команды
    /// </summary>
    [Flags]
    public enum ReturnCode : ushort
    {
        /// <summary>
        /// Успешное выполнение команды
        /// </summary>
        Successfull = 0x00,

        /// <summary>
        /// Безуспешное выполнение команды
        /// </summary>
        Failure = 0x01,

        /// <summary>
        /// Указанный sc-элемент не найден (не верный sc-адрес)
        /// </summary>
        AddressError = 0x02,
    }
}
