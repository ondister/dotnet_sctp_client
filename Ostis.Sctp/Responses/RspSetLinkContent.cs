using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspSetLinkContent:Response
    {
        private bool _contentisset = false;

        public bool ContentIsSet
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
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
