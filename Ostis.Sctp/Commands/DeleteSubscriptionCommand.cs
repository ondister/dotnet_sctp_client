using System;

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
			: base(0x0f, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<SubscriptionId>(id));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
