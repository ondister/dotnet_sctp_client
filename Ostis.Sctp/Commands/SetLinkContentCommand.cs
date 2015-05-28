using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class SetLinkContentCommand : Command
    {
        public SetLinkContentCommand(ScAddress linkaddress, LinkContent content)
            : base(0x0b, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(linkaddress));
            Arguments.Add(new Argument<UInt32>((uint)content.BytesStream.Length));
            Arguments.Add(new Argument<LinkContent>(content));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
