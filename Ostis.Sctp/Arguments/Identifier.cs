using System;
using System.Text;

namespace sctp_client.Arguments
{
    /// <summary>
    /// Идентификатор sc-элемента
    /// </summary>
    public struct Identifier:IArgument
    {
        private byte[] _bytesstream;

        /// <summary>
        /// Возвращает массив байт идентификатора
        /// </summary>
        /// <value>
        /// Массив байт
        /// </value>
        public byte[] BytesStream
        {
            get { return _bytesstream; }
        }

        /// <summary>
        /// Возвращает длину массива байт идентификатора
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public uint Length
        {
            get { return (uint)_bytesstream.Length; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Identifier"/>
        /// </summary>
        /// <param name="value">Значение идентификатора</param>
        public Identifier(String value)
        {
            UTF8Encoding txtcoder = new UTF8Encoding();
           _bytesstream = txtcoder.GetBytes(value);
        }

        public static implicit operator Identifier(String value)
        {
            Identifier tempid = new Identifier();
            UTF8Encoding txtcoder = new UTF8Encoding();
            tempid._bytesstream = txtcoder.GetBytes(value);
            return tempid;
        }

        /// <summary>
        /// Возвращает строковое представление идентификатора
        /// </summary>
        /// <returns>
        /// Объект типа <see cref="T:System.String" />, содержащий строковое представление идентификатора
        /// </returns>
        public override String ToString()
        {
            UTF8Encoding txtcoder = new UTF8Encoding();
            return txtcoder.GetString(_bytesstream);
        }

        
    }
}
