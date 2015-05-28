using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdSetSysId : ACommand
    {

        public CmdSetSysId(ScAddress address,    Identifier identifier)
            : base(0xa1,0)
        {
           
            UInt32 argsize = 0;

            Argument<ScAddress> _address = new Argument<ScAddress>(address);
            base.Arguments.Add(_address);

            UInt32 contlenght = (uint)identifier.BytesStream.Length;
            Argument<UInt32> _argidlenght = new Argument<UInt32>(contlenght);
            base.Arguments.Add(_argidlenght);

            Argument<Identifier> _argid = new Argument<Identifier>(identifier);
            base.Arguments.Add(_argid);

            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }

    }
}
