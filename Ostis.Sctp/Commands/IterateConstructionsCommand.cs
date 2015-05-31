using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
#warning На эту команду нет класса ответа!
    internal class IterateConstructionsCommand : Command
    {
		public IterateConstructionsCommand(ConstructionTemplate template)
			: base(0x0d, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ConstructionTemplate>(template));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
