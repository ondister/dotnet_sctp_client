using System;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
   
    internal class CmdGetStatistics : ACommand
    {

        public CmdGetStatistics(DateTimeUNIX starttime,DateTimeUNIX endtime)
            : base(0xa2,0)
        {
           
            UInt32 argsize = 0;

            Argument<DateTimeUNIX> argstt = new Argument<DateTimeUNIX>(starttime);
            base._arguments.Add(argstt);

            Argument<DateTimeUNIX> argent = new Argument<DateTimeUNIX>(endtime);
            base._arguments.Add(argent);

            foreach (IArgument arg in base._arguments)
            {
                argsize += arg.Length;
            }
            base._header.ArgSize = argsize;
        }


    }
}
