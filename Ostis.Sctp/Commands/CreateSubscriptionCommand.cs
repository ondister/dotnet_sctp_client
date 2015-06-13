using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание подписки на события.
    /// </summary>
    public class CreateSubscriptionCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="type">тип события</param>
        /// <param name="address">SC-адрес</param>
		public CreateSubscriptionCommand(EventsType type, ScAddress address)
            : base(CommandCode.CreateSubscription)
        {
            Arguments.Add (new EventsTypeArgument(type));
			Arguments.Add(address);
        }
    }
}
