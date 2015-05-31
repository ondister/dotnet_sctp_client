using System;
using System.Text;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Структра представляет контент sc-ссылки. Пока поддерживаются только строки
    /// </summary>
    public struct LinkContent
    {
        private byte[] bytes;
        private LinkContentType contentType;

        public byte[] Bytes
        { get { return bytes; } }

        /// <summary>
        /// Возвращает тип (OSTIS) контента ссылки. Пока поддерживается только текст
        /// </summary>
        public LinkContentType ContentType
        { get { return contentType; } }

        /// <summary>
        /// Конвертирует байтовое представление контента ссылки в строковое
        /// </summary>
        /// <param name="bytecontent">Массив байт ссылки</param>
        /// <returns></returns>
        public static string ConvertToString(byte[] bytecontent)
        {
#warning Кодировщик нужно вынести в private static.
            UTF8Encoding txtcoder = new UTF8Encoding();
            return txtcoder.GetString(bytecontent);
        }

        public LinkContent(String value)
        {
            contentType = LinkContentType.text;
            UTF8Encoding txtcoder = new UTF8Encoding();
            bytes = txtcoder.GetBytes(value);
        }

        public LinkContent(byte[] bytesstream)
		{
			contentType = LinkContentType.unknown;	
			bytes = bytesstream;
		}

		public LinkContent(double value)
		{
			contentType = LinkContentType.numeric;	
			bytes = BitConverter.GetBytes(value);
		}
       
        public static implicit operator LinkContent(String value)
        {
            UTF8Encoding txtcoder = new UTF8Encoding();
            return new LinkContent
            {
                contentType = LinkContentType.text,
                bytes = txtcoder.GetBytes(value),
            };
        }
    }
}
