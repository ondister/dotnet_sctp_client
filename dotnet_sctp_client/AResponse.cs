

namespace sctp_client
{
    /// <summary>
    /// Абстрактный класс ответа сервера
    /// </summary>
    public abstract class AResponse
    {
        private byte[] _bytes;
        private ResponseHeader _header;

        /// <summary>
        /// Конструктор класса <see cref="AResponse"/>
        /// </summary>
        /// <param name="bytesstream">Массив байт</param>
        public AResponse(byte[] bytesstream)
        {
            _bytes = bytesstream;
            byte[] _headerbytes = new byte[10];
            if (_bytes.Length >= 10)
            {
                for (int index = 0; index < 10; index++)
                {
                    _headerbytes[index] = _bytes[index];
                }
            }
            _header = new ResponseHeader(_headerbytes);
        }

        /// <summary>
        /// Возвращает массив байт
        /// </summary>
        public byte[] BytesStream
        {
            get { return _bytes; }
        }


        /// <summary>
        /// Возвращает заголовок ответа
        /// </summary>
        public ResponseHeader Header
        {
            get { return _header; }
        }
    }
}
