using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspCreateEventSubscription:AResponse
    {
        private SubScriptionId _id=new SubScriptionId();

        public SubScriptionId IDofSubscribe
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
					_id.ID=BitConverter.ToInt32(base.BytesStream, base.Header.Leight);
                    
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
