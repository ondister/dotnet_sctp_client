using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Удаление подписки на события.
    /// </summary>
    public class DeleteSubscriptionResponse : Response
    {
        private SubscriptionId subscriptionId;

        /// <summary>
        /// Идентификатор подписки.
        /// </summary>
        public SubscriptionId SubscriptionId
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
					subscriptionId.ID=BitConverter.ToInt32(Bytes, Header.Length);
				}
                return subscriptionId;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public DeleteSubscriptionResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
