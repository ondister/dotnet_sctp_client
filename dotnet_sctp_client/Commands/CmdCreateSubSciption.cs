using System;
using sctp_client.Arguments;

namespace sctp_client.Commands
{

    internal class CmdCreateSubScription : ACommand
    {

		public CmdCreateSubScription(EventsType Type, ScAddress address)
            : base(0x0e,0)
        {
           
            UInt32 argsize = 0; 

          
			Argument<EventsType> argt = new Argument<EventsType> (Type);
			Argument<ScAddress> argf = new Argument<ScAddress>(address);
            
			base._arguments.Add (argt);
			base._arguments.Add(argf);
            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }


    }
}
