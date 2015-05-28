using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class SetSystemIdCommand : Command
    {
        public SetSystemIdCommand(ScAddress address, Identifier identifier)
            : base(0xa1, 0)
        {
            UInt32 argumentsSize = 0;
            Argument<ScAddress> _address = new Argument<ScAddress>(address);
            Arguments.Add(_address);
            UInt32 contlenght = (uint)identifier.BytesStream.Length;
            Argument<UInt32> _argidlenght = new Argument<UInt32>(contlenght);
            Arguments.Add(_argidlenght);
            Argument<Identifier> _argid = new Argument<Identifier>(identifier);
            Arguments.Add(_argid);
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
