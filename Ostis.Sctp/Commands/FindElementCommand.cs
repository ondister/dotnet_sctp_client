using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class FindElementCommand : Command
    {
        public FindElementCommand(Identifier identifier)
            : base(0xa0, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<UInt32>((uint) identifier.BytesStream.Length));
            Arguments.Add(new Argument<Identifier>(identifier));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
