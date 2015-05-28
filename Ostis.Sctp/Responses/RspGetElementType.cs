using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspGetElementType:Response
    {
        private ElementType _elementtype = ElementType.unknown;

        public ElementType ElementType
        {
            get 
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
               _elementtype=(ElementType)BitConverter.ToUInt16(base.BytesStream, base.Header.Length);
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
