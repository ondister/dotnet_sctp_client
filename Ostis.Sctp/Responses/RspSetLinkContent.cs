using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspSetLinkContent:AResponse
    {
        private bool _contentisset = false;

        public bool ContentIsSet
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                    _contentisset = true;
                }
                else
                {
                    _contentisset = false;
                }
                return _contentisset;
            }
        }


        public RspSetLinkContent(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
