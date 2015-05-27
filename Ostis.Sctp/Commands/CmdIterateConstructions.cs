using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdIterateConstructions : ACommand
    {

		public CmdIterateConstructions(ConstrTemplate iteratetemplate)
			: base(0x0d,0)
        {
           
            UInt32 argsize = 0;

            Argument<ConstrTemplate> argf = new Argument<ConstrTemplate>(iteratetemplate);
            base._arguments.Add(argf);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgumentsSize = argsize;
        }


    }
}
