using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание подписки на события.
    /// </summary>
    public class CreateSubscriptionResponse : Response
    {
        private readonly SubscriptionId subscriptionId= new SubscriptionId(0);

        /// <summary>
        /// Идентификатор подписки.
        /// </summary>
        public SubscriptionId SubscriptionId
        { get { return subscriptionId; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateSubscriptionResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                subscriptionId = SubscriptionId.Parse(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
