namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор SC-элемента.
    /// </summary>
    public struct Identifier : IArgument
    {
        private readonly byte[] bytes;

        /// <summary>
        /// Массив байт идентификатора.
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="value">значение</param>
        public Identifier(string value)
        {
            bytes = SctpProtocol.TextEncoding.GetBytes(value);
        }

        /// <summary>
        /// Преобразование из строки.
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
            return SctpProtocol.TextEncoding.GetString(bytes);
        }
    }
}
