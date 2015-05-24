using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdCreateNode : ACommand
    {

        public CmdCreateNode(ElementType nodetype)
            : base(0x04,0)
        {
           
            UInt32 argsize = 0;

            Argument<ElementType> argf = new Argument<ElementType>(nodetype);
            base._arguments.Add(argf);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }


    }
}
