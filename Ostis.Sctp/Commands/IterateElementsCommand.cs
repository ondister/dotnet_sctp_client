using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class IterateElementsCommand : Command
    {
        public IterateElementsCommand(ConstrTemplate template)
            : base(0x0c, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ConstrTemplate>(template));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
