using System;
using System.Text;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор sc-элемента
    /// </summary>
    public struct Identifier : IArgument
    {
        private byte[] bytes;

        /// <summary>
        /// Возвращает массив байт идентификатора
        /// </summary>
        /// <value>
        /// Массив байт
        /// </value>
        public byte[] BytesStream
        { get { return bytes; } }

#warning Удалить это свойство - оно лишнее.
        /// <summary>
        /// Возвращает длину массива байт идентификатора
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public uint Length
        { get { return (uint) bytes.Length; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Identifier"/>
        /// </summary>
        /// <param name="value">Значение идентификатора</param>
        public Identifier(String value)
        {
#warning Кодировщик нужно вынести в private static.
            UTF8Encoding txtcoder = new UTF8Encoding();
            bytes = txtcoder.GetBytes(value);
        }

        public static implicit operator Identifier(String value)
        {
            var identifier = new Identifier();
            UTF8Encoding txtcoder = new UTF8Encoding();
            identifier.bytes = txtcoder.GetBytes(value);
            return identifier;
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
            return txtcoder.GetString(bytes);
        }
    }
}
