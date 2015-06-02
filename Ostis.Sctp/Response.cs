﻿namespace Ostis.Sctp
{
    /// <summary>
    /// Базовый класс ответа.
    /// </summary>
    public abstract class Response
    {
        private readonly byte[] bytes;
        private readonly ResponseHeader header;

        /// <summary>
        /// Массив байт.
        /// </summary>
        public byte[] Bytes
        { get { return bytes; } }

        /// <summary>
        /// Заголовок.
        /// </summary>
        public ResponseHeader Header
        { get { return header; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт ответа</param>
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
    }
}