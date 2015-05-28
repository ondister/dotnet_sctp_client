using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class GetStatisticsCommand : Command
    {
        public GetStatisticsCommand(DateTimeUNIX startTime, DateTimeUNIX endTime)
            : base(0xa2, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<DateTimeUNIX>(startTime));
            Arguments.Add(new Argument<DateTimeUNIX>(endTime));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
