using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
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
