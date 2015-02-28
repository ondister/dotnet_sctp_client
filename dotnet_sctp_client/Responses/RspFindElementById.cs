using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspFindElementById:AResponse
    {
        private ScAddress _address=new ScAddress();
        private bool _isfounded;

        public bool IsFounded
        {
            get {
                _isfounded = false;
                if (base.Header.ReturnSize != 0) { _isfounded = true; }
                return _isfounded; 
            }
        }
        public ScAddress FindedScAddress
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


        public RspFindElementById(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
