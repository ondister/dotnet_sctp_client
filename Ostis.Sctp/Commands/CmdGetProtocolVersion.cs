using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdGetProtocolVersion : ACommand
    {

		public CmdGetProtocolVersion()
            : base(0xa3,0)
        {
           
            UInt32 argsize = 0;

            base._header.ArgumentsSize = argsize;
        }


    }
}
