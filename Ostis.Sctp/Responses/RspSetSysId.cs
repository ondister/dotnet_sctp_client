using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspSetSysId:AResponse
    {
        private bool _issetsuccessfull = false;

        public bool IsSetSuccesfull
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                    _issetsuccessfull = true;
                }
                else
                {
                    _issetsuccessfull = false;
                }
                return _issetsuccessfull;
            }
        }

        public RspSetSysId(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
