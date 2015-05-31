using System;

namespace Ostis.Sctp
{
    /// <summary>
    /// Аргумент команды.
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Длина массива байт.
        /// </summary>
        UInt32 Length
        { get; }
#warning Удалить это свойство - оно лишнее.

        /// <summary>
        /// Массив байт.
        /// </summary>
#warning Переименовать в Bytes.
        byte[] BytesStream
        { get; }
    }
}
