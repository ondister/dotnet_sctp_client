using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class GetElementTypeResponse : Response
    {
        private ElementType elementType = ElementType.unknown;

        public ElementType ElementType
        {
            get 
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    elementType = (ElementType) BitConverter.ToUInt16(BytesStream, Header.Length);
                }
                return elementType; 
            }
        }

        public GetElementTypeResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
