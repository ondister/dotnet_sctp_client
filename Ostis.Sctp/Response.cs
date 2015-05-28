namespace Ostis.Sctp
{
    /// <summary>
    /// Абстрактный класс ответа сервера
    /// </summary>
    public abstract class Response
    {
        private readonly byte[] bytes;
        private readonly ResponseHeader header;

        /// <summary>
        /// Конструктор класса <see cref="Response"/>
        /// </summary>
        /// <param name="bytes">Массив байт</param>
        protected Response(byte[] bytes)
        {
            this.bytes = bytes;
            var headerBytes = new byte[10];
            if (bytes.Length >= 10)
            {
                for (int index = 0; index < 10; index++)
                {
                    headerBytes[index] = bytes[index];
                }
            }
            header = new ResponseHeader(headerBytes);
        }

        /// <summary>
        /// Возвращает массив байт
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

        /// <summary>
        /// Возвращает заголовок ответа
        /// </summary>
        public ResponseHeader Header
        { get { return header; } }
    }
}
