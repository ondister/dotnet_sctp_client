﻿using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdDeleteSubScription : ACommand
    {

		public CmdDeleteSubScription(SubScriptionId id)
			: base(0x0f,0)
        {
           
            UInt32 argsize = 0;

          
			Argument<SubScriptionId> argt = new Argument<SubScriptionId> (id);
            
			base._arguments.Add (argt);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgumentsSize = argsize;
        }


    }
}
