using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspGetElementType:AResponse
    {
        private ElementType _elementtype = ElementType.unknown;

        public ElementType ElementType
        {
            get 
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
               _elementtype=(ElementType)BitConverter.ToUInt16(base.BytesStream, base.Header.Leight);
                }
                return _elementtype; 
            }
        }

      
        public RspGetElementType(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
