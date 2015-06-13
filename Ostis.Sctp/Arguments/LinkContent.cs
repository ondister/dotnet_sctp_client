using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Содержимое SC-ссылки (пока поддерживаются только строки).
    /// </summary>
    public struct LinkContent : IArgument
    {
        private byte[] data;
        private LinkContentType contentType;

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

#warning Конструкторы не вызывают один другой.
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(string value)
        {
            contentType = LinkContentType.Text;
            data = SctpProtocol.TextEncoding.GetBytes(value);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public LinkContent(byte[] bytes)
		{
			contentType = LinkContentType.Unknown;	
			data = bytes;
		}

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(double value)
		{
			contentType = LinkContentType.Numeric;	
			data = BitConverter.GetBytes(value);
		}

        /// <summary>
        /// Преобразование из строки.
        /// </summary>
        /// <param name="value">строковое значение</param>
        /// <returns>соднржимое ссылки+</returns>
        public static implicit operator LinkContent(string value)
        {
            return new LinkContent
            {
                contentType = LinkContentType.Text,
                data = SctpProtocol.TextEncoding.GetBytes(value),
            };
        }

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
