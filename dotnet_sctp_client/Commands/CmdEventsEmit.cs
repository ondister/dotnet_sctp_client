using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
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
