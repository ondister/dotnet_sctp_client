using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Содержимое SC-ссылки (пока поддерживаются только строки).
    /// </summary>
    public struct LinkContent
    {
        private byte[] bytes;
        private LinkContentType contentType;

        /// <summary>
        /// Массив байт.
        /// </summary>
        public byte[] Bytes
        { get { return bytes; } }

        /// <summary>
        /// OSTIS-тип содержимого ссылки.
        /// </summary>
        public LinkContentType ContentType
        { get { return contentType; } }

        /// <summary>
        /// Преобразование байтового представления содержимого ссылки в строковое.
        /// </summary>
        /// <param name="byteContent">массив байт ссылки</param>
        /// <returns>строка</returns>
        public static string ConvertToString(byte[] byteContent)
        {
            return SctpProtocol.TextEncoding.GetString(byteContent);
        }

#warning Конструкторы не вызывают один другой.
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(string value)
        {
            contentType = LinkContentType.Text;
            bytes = SctpProtocol.TextEncoding.GetBytes(value);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public LinkContent(byte[] bytes)
		{
			contentType = LinkContentType.Unknown;	
			this.bytes = bytes;
		}

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(double value)
		{
			contentType = LinkContentType.Numeric;	
			bytes = BitConverter.GetBytes(value);
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
                bytes = SctpProtocol.TextEncoding.GetBytes(value),
            };
        }
    }
}
