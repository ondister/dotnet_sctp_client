using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspGetArc:AResponse
    {
        private ScAddress _baddress=new ScAddress();
		private ScAddress _eaddress=new ScAddress();
        public ScAddress BeginElementAddress
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                   
                    _baddress.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight + 2);
                    _baddress.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight);
                }

                return _baddress;
            }
        }

		public ScAddress EndElementAddress
		{
			get
			{
				if (base.Header.ReturnCode == enumReturnCode.Successfull)
				{

					_eaddress.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight + 6);
					_eaddress.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight+4);
				}

				return _eaddress;
			}
		}

        public RspGetArc(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
