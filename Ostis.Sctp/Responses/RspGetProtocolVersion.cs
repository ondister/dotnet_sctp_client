using System;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class GetProtocolVersionResponse : Response
    {
       
		Int32 protocolVersion;

		public Int32 ProtocolVersion
		{
			get
			{
				if (Header.ReturnCode == ReturnCode.Successfull)
				{
					protocolVersion = BitConverter.ToInt32(BytesStream, Header.Length);
				}
				return protocolVersion;
			}
		}

        public GetProtocolVersionResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
