using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class CreateSubscriptionCommand : Command
    {
		public CreateSubscriptionCommand(EventsType Type, ScAddress address)
            : base(0x0e ,0)
        {
            UInt32 argumentsSize = 0; 
            Arguments.Add (new Argument<EventsType>(Type));
			Arguments.Add(new Argument<ScAddress>(address));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }


    }
}
