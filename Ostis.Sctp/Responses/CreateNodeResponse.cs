using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

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
                createdNodeAddress.Offset = BitConverter.ToUInt16(bytes, Header.Length + 2);
                createdNodeAddress.Segment = BitConverter.ToUInt16(bytes, Header.Length);
            }
        }
    }
}
