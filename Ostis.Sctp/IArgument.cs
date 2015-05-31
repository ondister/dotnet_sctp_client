using System;

namespace Ostis.Sctp
{
    /// <summary>
    /// Интерфейс аргумента для команд
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Возвращает длину массива байт аргумента
        /// </summary>
        UInt32 Length
        { get; }

        /// <summary>
        /// Возвращает массив байт аргумента
        /// </summary>
        /// <value>
#warning Переименовать в Bytes
        byte[] BytesStream
        { get; }
    }
}
