using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspGetArc:AResponse
    {
        private ScAddress _baddress=new ScAddress();
		private ScAddress _eaddress=new ScAddress();
        public ScAddress BeginElementAddress
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
                   
                    _baddress.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Length + 2);
                    _baddress.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Length);
                }

                return _baddress;
            }
        }

		public ScAddress EndElementAddress
		{
			get
			{
				if (base.Header.ReturnCode == ReturnCode.Successfull)
				{

					_eaddress.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Length + 6);
					_eaddress.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Length+4);
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
