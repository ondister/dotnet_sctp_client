using System;
using System.Text;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор SC-элемента.
    /// </summary>
    public struct Identifier : IArgument
    {
        private byte[] bytes;

        /// <summary>
        /// Массив байт идентификатора.
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

#warning Удалить это свойство - оно лишнее.
        /// <summary>
        /// Длина массива байт идентификатора.
        /// </summary>
        public uint Length
        { get { return (uint) bytes.Length; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public Identifier(String value)
        {
#warning Кодировщик нужно вынести в private static.
            UTF8Encoding txtcoder = new UTF8Encoding();
            bytes = txtcoder.GetBytes(value);
        }

        /// <summary>
        /// Преобразование из строки.
        /// </summary>
        /// <param name="value">строковое значение</param>
        /// <returns>SC-идентификатор</returns>
        public static implicit operator Identifier(String value)
        {
            var identifier = new Identifier();
            UTF8Encoding txtcoder = new UTF8Encoding();
            identifier.bytes = txtcoder.GetBytes(value);
            return identifier;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override String ToString()
        {
            UTF8Encoding txtcoder = new UTF8Encoding();
            return txtcoder.GetString(bytes);
        }
    }
}
