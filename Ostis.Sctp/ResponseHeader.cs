using System;

namespace Ostis.Sctp
{
    /// <summary>
    /// Заголовок ответа сервера.
    /// </summary>
    public class ResponseHeader
    {
        private readonly CommandCode commandCode;
        private readonly uint id;
        private readonly ReturnCode returnCode;
        private readonly uint returnSize;

        /// <summary>
        /// Код команды, на которую получен ответ.
        /// </summary>
        public CommandCode CommandCode
        { get { return commandCode; } }

        /// <summary>
        /// Уникальный идентификатор команды в потоке команд.
        /// </summary>
        public uint Id
        { get { return id; } }

        /// <summary>
        /// Код результата выполнения команды.
        /// </summary>
        public ReturnCode ReturnCode
        { get { return returnCode; } }

        /// <summary>
        /// Размер содержимого ответа.
        /// </summary>
        public uint ReturnSize
        { get { return returnSize; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт заголовка</param>
        public ResponseHeader(byte[] bytes)
        {
            if (bytes.Length >= SctpProtocol.HeaderLength)
            {
                commandCode = (CommandCode) bytes[0];
                id = BitConverter.ToUInt32(bytes, sizeof(CommandCode));
				returnCode = (ReturnCode) bytes[sizeof(CommandCode) + sizeof(uint)];
                returnSize = BitConverter.ToUInt32(bytes, sizeof(CommandCode) + sizeof(uint) + sizeof(ReturnCode));
            }
            else
            {
                commandCode = CommandCode.Unknown;
                id = default(uint);
                returnCode = default(ReturnCode);
                returnSize = default(uint);
            }
        }
    }
}
