using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkResponse : Response
    {
        private readonly ScAddress createdLinkAddress;

        /// <summary>
        /// Адрес созданной ссылки.
        /// </summary>
        public ScAddress CreatedLinkAddress
        { get { return createdLinkAddress; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateLinkResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                createdLinkAddress.Offset = BitConverter.ToUInt16(bytes, Header.Length + 2);
                createdLinkAddress.Segment = BitConverter.ToUInt16(bytes, Header.Length);
            }
        }
    }
}
