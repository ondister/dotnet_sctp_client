using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Содержимое SC-ссылки (пока поддерживаются только строки).
    /// </summary>
    public class LinkContent : IArgument
    {
        /// <summary>
        /// OSTIS-тип содержимого ссылки.
        /// </summary>
        public LinkContentType ContentType
        { get { return contentType; } }

        /// <summary>
        /// Содержимое.
        /// </summary>
        public byte[] Data
        { get { return data; } }

        private readonly LinkContentType contentType;
        private readonly byte[] data;

        #region Конструкторы

        private LinkContent(LinkContentType contentType, byte[] data)
        {
            this.contentType = contentType;
            this.data = data;
        }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из массива байт.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public LinkContent(byte[] bytes)
            : this(LinkContentType.Unknown, bytes)
        { }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из строки.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(string value)
            : this(LinkContentType.Text, SctpProtocol.TextEncoding.GetBytes(value))
        { }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из типа Double.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(double value)
            : this(LinkContentType.Numeric, BitConverter.GetBytes(value))
		{ }

        #endregion

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(data.Length));
            bytes.AddRange(data);
            return bytes.ToArray();
        }

        #endregion
    }
}
