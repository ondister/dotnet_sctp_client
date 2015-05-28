using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;

namespace Ostis.Sctp.Responses
{
    public class RspCreateEventSubscription:Response
    {
        private SubScriptionId _id=new SubScriptionId();

        public SubScriptionId IDofSubscribe
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
					_id.ID=BitConverter.ToInt32(base.BytesStream, base.Header.Length);
                    
				}

                return _id;
            }
        }


		public RspCreateEventSubscription(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
