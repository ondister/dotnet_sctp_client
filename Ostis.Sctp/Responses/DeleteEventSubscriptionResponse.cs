using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class DeleteSubscriptionResponse : Response
    {
        private SubscriptionId subscriptionId;

        public SubscriptionId SubscriptionId
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

        public DeleteSubscriptionResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
