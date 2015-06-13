using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание подписки на события.
    /// </summary>
    public class CreateSubscriptionResponse : Response
    {
        private readonly SubscriptionId subscriptionId;

        /// <summary>
        /// Идентификатор подписки.
        /// </summary>
        public SubscriptionId SubscriptionId
        { get { return subscriptionId; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateSubscriptionResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                subscriptionId.Id = BitConverter.ToInt32(bytes, Header.Length);
            }
        }
    }
}
