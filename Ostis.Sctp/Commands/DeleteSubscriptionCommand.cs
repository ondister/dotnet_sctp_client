using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class DeleteSubscriptionCommand : Command
    {
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
