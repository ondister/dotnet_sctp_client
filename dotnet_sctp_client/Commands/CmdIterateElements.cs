using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{

    internal class CmdIterateElements : ACommand
    {

        public CmdIterateElements(ConstrTemplate iteratetemplate)
            : base(0x0c,0)
        {
           
            UInt32 argsize = 0;

            Argument<ConstrTemplate> argf = new Argument<ConstrTemplate>(iteratetemplate);
            base._arguments.Add(argf);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }


    }
}
