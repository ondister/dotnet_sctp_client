using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class CreateArcResponse : Response
    {
#warning Это поле - явно лишнее.
        private ScAddress address;

        public ScAddress CreatedArcAddress
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

        public CreateArcResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
