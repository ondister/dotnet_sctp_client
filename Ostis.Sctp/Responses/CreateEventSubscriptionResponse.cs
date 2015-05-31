using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class CreateSubscriptionResponse : Response
    {
        private SubScriptionId subscriptionId;

        public SubScriptionId IDofSubscribe
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
					subscriptionId.ID=BitConverter.ToInt32(BytesStream, Header.Length);
				}
                return subscriptionId;
            }
        }

        public CreateSubscriptionResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
