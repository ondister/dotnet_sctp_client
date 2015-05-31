using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class CreateLinkResponse : Response
    {
        private ScAddress address;

        public ScAddress CreatedLinkAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    address.Offset = BitConverter.ToUInt16(BytesStream, Header.Length + 2);
                    address.Segment = BitConverter.ToUInt16(BytesStream, Header.Length);
                }
                return address;
            }
        }

        public CreateLinkResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
