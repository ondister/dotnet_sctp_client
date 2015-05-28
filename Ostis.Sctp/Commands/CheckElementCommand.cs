﻿using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
   
    internal class CmdCheckElement : ACommand
    {
        
        public CmdCheckElement(ScAddress address)
            : base(0x01,0)
        {
           
            UInt32 argsize = 0;

            Argument<ScAddress> argf = new Argument<ScAddress>(address);
            base.Arguments.Add(argf);
            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }


    }
}