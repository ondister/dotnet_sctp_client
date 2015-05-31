using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class FindElementResponse : Response
    {
        private ScAddress address;
        private bool isFound;

        public bool IsFound
        {
            get
            {
                isFound = Header.ReturnSize != 0;
                return isFound;
            }
        }

        public ScAddress FindedScAddress
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

        public FindElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
