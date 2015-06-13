using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание нового SC-узла указанного типа.
    /// </summary>
    public class CreateNodeResponse : Response
    {
        private readonly ScAddress createdNodeAddress;

        /// <summary>
        /// Адрес созданного узла.
        /// </summary>
        public ScAddress CreatedNodeAddress
        { get { return createdNodeAddress; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateNodeResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
#warning Использовать метод Parse
                createdNodeAddress.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 2);
                createdNodeAddress.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
