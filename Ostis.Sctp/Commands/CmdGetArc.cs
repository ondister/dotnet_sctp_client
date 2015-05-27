using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdGetArc : ACommand
    {

        public CmdGetArc(ScAddress arcaddress)
            : base(0x07,0)
        {
           
            UInt32 argsize = 0;

            Argument<ScAddress> _arcaddress = new Argument<ScAddress>(arcaddress);
            base._arguments.Add(_arcaddress);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgumentsSize = argsize;
        }


    }
}
