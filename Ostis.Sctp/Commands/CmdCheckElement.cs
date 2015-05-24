using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{
   
    internal class CmdCheckElement : ACommand
    {
        
        public CmdCheckElement(ScAddress address)
            : base(0x01,0)
        {
           
            UInt32 argsize = 0;

            Argument<ScAddress> argf = new Argument<ScAddress>(address);
            base._arguments.Add(argf);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }


    }
}
