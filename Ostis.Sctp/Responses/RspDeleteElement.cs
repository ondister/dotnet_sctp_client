using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspDeleteElement:Response
    {
        private bool _elementisdelete = false;

        public bool ElementIsDelete
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
                    _elementisdelete = true;
                }
                else
                {
                    _elementisdelete = false;
                }
                return _elementisdelete;
            }
        }


        public RspDeleteElement(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
