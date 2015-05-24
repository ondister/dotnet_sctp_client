using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspDeleteElement:AResponse
    {
        private bool _elementisdelete = false;

        public bool ElementIsDelete
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
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
