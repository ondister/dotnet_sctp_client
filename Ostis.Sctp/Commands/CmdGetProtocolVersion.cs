using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{

    internal class CmdGetProtocolVersion : ACommand
    {

		public CmdGetProtocolVersion()
            : base(0xa3,0)
        {
           
            UInt32 argsize = 0;

            base._header.ArgSize = argsize;
        }


    }
}
