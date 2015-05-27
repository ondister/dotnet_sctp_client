using System;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    /// <summary>
    /// Заголовок ответа сервера
    /// </summary>
    public class ResponseHeader
    {
        private readonly byte code;
        private readonly UInt32 id;
        private readonly ReturnCode returnCode;
        private readonly UInt32 returnSize;
        private readonly int length;

        /// <summary>
        /// Длина заголовка
        /// </summary>
        public int Length
        { get { return length; } }

        /// <summary>
        /// Код команды, на которую получен ответ
        /// </summary>
        public byte Code
        { get { return code; } }

        /// <summary>
        /// Уникальный идентификатор команды в потоке команд
        /// </summary>
        public UInt32 Id
        { get { return id; } }

        /// <summary>
        ///Возвращает код успешности выполнения команды
        /// </summary>
        public ReturnCode ReturnCode
        { get { return returnCode; } }

        /// <summary>
        /// Возвращает размер содержимого ответа
        /// </summary>
        public UInt32 ReturnSize
        { get { return returnSize; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResponseHeader"/>
        /// </summary>
        /// <param name="bytes">Массив байт заголовка</param>
        public ResponseHeader(byte[] bytes)
        {
            if (bytes.Length >= 10)
            {
                code = bytes[0];
                id = BitConverter.ToUInt32(bytes, 1);
                returnCode = (ReturnCode) bytes[5];
                returnSize = BitConverter.ToUInt32(bytes, 6);
                length = bytes.Length;
            }
        }
    }
}
