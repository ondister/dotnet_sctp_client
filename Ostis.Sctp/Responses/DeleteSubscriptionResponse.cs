using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Удаление подписки на события.
    /// </summary>
    public class DeleteSubscriptionResponse : Response
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
        public DeleteSubscriptionResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                subscriptionId = SubscriptionId.Parse(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
