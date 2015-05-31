using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду CreateLinkCommand.
    /// </summary>
    public class CreateLinkResponse : Response
    {
        private ScAddress address;

        /// <summary>
        /// Адрес созданной ссылки.
        /// </summary>
        public ScAddress CreatedLinkAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    address.Offset = BitConverter.ToUInt16(Bytes, Header.Length + 2);
                    address.Segment = BitConverter.ToUInt16(Bytes, Header.Length);
                }
                return address;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateLinkResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
