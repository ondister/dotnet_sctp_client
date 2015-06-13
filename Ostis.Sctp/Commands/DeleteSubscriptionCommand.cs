using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Удаление подписки на события.
    /// </summary>
    public class DeleteSubscriptionCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="id">событие</param>
		public DeleteSubscriptionCommand(SubscriptionId id)
			: base(CommandCode.DeleteSubscription, 0)
        {
            Arguments.Add(new Argument<SubscriptionId>(id));
        }
    }
}
