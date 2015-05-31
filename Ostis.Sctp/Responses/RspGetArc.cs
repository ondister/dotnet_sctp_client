using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class GetArcResponse : Response
    {
        private ScAddress beginElementAddress;
        private ScAddress endElementAddress;

        public ScAddress BeginElementAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    beginElementAddress.Offset = BitConverter.ToUInt16(BytesStream, Header.Length + 2);
                    beginElementAddress.Segment = BitConverter.ToUInt16(BytesStream, Header.Length);
                }
                return beginElementAddress;
            }
        }

		public ScAddress EndElementAddress
		{
			get
			{
				if (Header.ReturnCode == ReturnCode.Successfull)
				{
                    endElementAddress.Offset = BitConverter.ToUInt16(BytesStream, Header.Length + 6);
                    endElementAddress.Segment = BitConverter.ToUInt16(BytesStream, Header.Length + 4);
				}
                return endElementAddress;
			}
		}

        public GetArcResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
