using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор SC-элемента.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="Identifier"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="Identifier" lang="C#" />
    /// </example>
    public class Identifier : IArgument
    {
        /// <summary>
        /// Значение.
        /// </summary>
        public string Value
        { get { return value; } }

        private readonly string value;

        /// <summary>
        /// Инициализирует новый идентификатор SC-элемента.
        /// </summary>
        /// <param name="value">значение</param>
        public Identifier(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Преобразование Идентификатора из строки.
        /// </summary>
        /// <param name="value">строковое значение</param>
        /// <returns>SC-идентификатор</returns>
        public static implicit operator Identifier(string value)
        {
            return new Identifier(value);
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return value;
        }

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytesWithoutLength = SctpProtocol.TextEncoding.GetBytes(value);
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(bytesWithoutLength.Length));
            bytes.AddRange(bytesWithoutLength);
            return bytes.ToArray();
        }

        #endregion
    }
}
