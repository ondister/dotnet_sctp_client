using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspCreateArc:AResponse
    {
        private ScAddress _address=new ScAddress();

        public ScAddress CreatedArcAddress
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                   
                    _address.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight + 2);
                    _address.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight);
                }

                return _address;
            }
        }


        public RspCreateArc(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
