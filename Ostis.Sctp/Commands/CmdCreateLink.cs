using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{

    internal class CmdCreateLink : ACommand
    {

        public CmdCreateLink()
            : base(0x05,0)
        {
           
            UInt32 argsize = 0;

            base._header.ArgSize = argsize;
        }


    }
}
