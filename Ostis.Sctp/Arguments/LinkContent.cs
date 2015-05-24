using System;
using System.Text;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Структра представляет контент sc-ссылки. Пока поддерживаются только строки
    /// </summary>
    public struct LinkContent
    {
        private byte[] _bytesstream;

        public byte[] BytesStream
        {
            get { return _bytesstream; }
        }


        private LinkContentType _contenttype;

        /// <summary>
        /// Возвращает тип (OSTIS) контента ссылки. Пока поддерживается только текст
        /// </summary>
        
        public LinkContentType ContentType
        {
            get { return _contenttype; }
        }

        /// <summary>
        /// Конвертирует байтовое представление контента ссылки в строковое
        /// </summary>
        /// <param name="bytecontent">Массив байт ссылки</param>
        /// <returns></returns>
        public static string ConvertToString(byte[] bytecontent)
        { 
         UTF8Encoding txtcoder = new UTF8Encoding();
         return txtcoder.GetString(bytecontent);
        }

        public LinkContent(String value)
        {
           _contenttype = LinkContentType.text;
            UTF8Encoding txtcoder = new UTF8Encoding();
           _bytesstream = txtcoder.GetBytes(value);
        }
       
		public LinkContent(byte[] bytesstream)
		{
			_contenttype = LinkContentType.unknown;	
			_bytesstream = bytesstream;
		}

		public LinkContent(double value)
		{
			_contenttype = LinkContentType.numeric;	
			_bytesstream = BitConverter.GetBytes(value);
		}


       
        public static implicit operator LinkContent(String value)
        {
            LinkContent templc = new LinkContent();
            templc._contenttype = LinkContentType.text;
            UTF8Encoding txtcoder = new UTF8Encoding();
            templc._bytesstream = txtcoder.GetBytes(value);
            return templc;
        }

       

    }
}
