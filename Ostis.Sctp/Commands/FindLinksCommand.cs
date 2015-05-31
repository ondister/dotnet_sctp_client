using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class FindLinksCommand : Command
    {
        public FindLinksCommand(LinkContent content)
            : base(0x0a, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<UInt32>((uint)content.Bytes.Length));
            Arguments.Add(new Argument<LinkContent>(content));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
