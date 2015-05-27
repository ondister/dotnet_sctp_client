using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
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
                   
                    _address.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Length + 2);
                    _address.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Length);
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
