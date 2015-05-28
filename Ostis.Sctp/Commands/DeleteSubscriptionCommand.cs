using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class DeleteSubscriptionCommand : Command
    {
		public DeleteSubscriptionCommand(SubScriptionId id)
			: base(0x0f, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<SubScriptionId>(id));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
