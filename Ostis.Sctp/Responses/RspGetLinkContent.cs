using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspGetLInkContent:AResponse
    {
        private byte[] _linkcontent;

        public byte[] LinkContent
        {
            get { return _linkcontent; }
        }



        public RspGetLInkContent(byte[] bytesstream)
            : base(bytesstream)
        {
            if (base.Header.ReturnSize != 0)
            {
                _linkcontent = new byte[base.Header.ReturnSize];
                Array.Copy(base.BytesStream, base.Header.Leight, _linkcontent, 0, _linkcontent.Length);
            }
            else
            { _linkcontent = new byte[0]; }
        }

     
    }
}
