using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspSetSysId:AResponse
    {
        private bool _issetsuccessfull = false;

        public bool IsSetSuccesfull
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
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
