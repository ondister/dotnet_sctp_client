using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspCreateNode:AResponse
    {
        private ScAddress _address=new ScAddress();

        public ScAddress CreatedNodeAddress
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
                   
                    _address.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Length + 2);
                    _address.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Length);
                }

                return _address;
            }
        }


        public RspCreateNode(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
