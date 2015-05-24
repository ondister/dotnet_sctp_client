using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdEventsEmit : ACommand
    {

		public CmdEventsEmit()
            : base(0x10,0)
        {
           
            UInt32 argsize = 0;

            base._header.ArgSize = argsize;
        }


    }
}
