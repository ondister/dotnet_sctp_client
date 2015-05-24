using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sctp_client
{
    /// <summary>
    /// Интерфейс аргумента для команд
    /// </summary>
  public interface IArgument
    {
        /// <summary>
        /// Возвращает длину массива байт аргумента
        /// </summary>
     UInt32 Length { get; }
     /// <summary>
     /// Возвращает массив байт аргумента
     /// </summary>
     /// <value>
     byte[] BytesStream { get; }
    }
}
