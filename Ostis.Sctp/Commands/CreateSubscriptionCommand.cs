using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{

    internal class CmdCreateSubScription : ACommand
    {

		public CmdCreateSubScription(EventsType Type, ScAddress address)
            : base(0x0e,0)
        {
           
            UInt32 argsize = 0; 

          
			Argument<EventsType> argt = new Argument<EventsType> (Type);
			Argument<ScAddress> argf = new Argument<ScAddress>(address);
            
			base.Arguments.Add (argt);
			base.Arguments.Add(argf);
            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }


    }
}
