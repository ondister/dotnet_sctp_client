using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class CreateNodeResponse : Response
    {
        private ScAddress address;

        public ScAddress CreatedNodeAddress
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
                   
                    address.Offset = BitConverter.ToUInt16(BytesStream, Header.Length + 2);
                    address.Segment = BitConverter.ToUInt16(BytesStream, Header.Length);
                }

                return address;
            }
        }

        public CreateNodeResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
