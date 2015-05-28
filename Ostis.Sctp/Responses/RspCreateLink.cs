using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspCreateLink:Response
    {
        private ScAddress _address=new ScAddress();

        public ScAddress CreatedLinkAddress
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


        public RspCreateLink(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
