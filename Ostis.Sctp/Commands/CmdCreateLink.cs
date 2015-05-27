using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdCreateLink : ACommand
    {

        public CmdCreateLink()
            : base(0x05,0)
        {
           
            UInt32 argsize = 0;

            base._header.ArgumentsSize = argsize;
        }


    }
}
