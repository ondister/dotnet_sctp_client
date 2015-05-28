using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspGetProtocolVersion:Response
    {
       
		Int32 _protocolversion=0;

		public Int32 ProtocolVersion
		{

			get
			{
				if (base.Header.ReturnCode == ReturnCode.Successfull)
				{
					_protocolversion = BitConverter.ToInt32(base.BytesStream, base.Header.Length);
				}

				return _protocolversion;
			}
		}

        public RspGetProtocolVersion(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
