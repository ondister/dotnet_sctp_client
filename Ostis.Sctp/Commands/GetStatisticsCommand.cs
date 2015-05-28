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
            base.Arguments.Add(argstt);

            Argument<DateTimeUNIX> argent = new Argument<DateTimeUNIX>(endtime);
            base.Arguments.Add(argent);

            foreach (IArgument arg in base.Arguments)
            {
                argsize += arg.Length;
            }
            base.Header.ArgumentsSize = argsize;
        }


    }
}
