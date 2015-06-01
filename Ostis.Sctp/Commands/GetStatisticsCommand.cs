using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение статистики с сервера, во временных границах.
    /// </summary>
    public class GetStatisticsCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="startTime">нижняя временная граница</param>
        /// <param name="endTime">верхняя временная граница</param>
        public GetStatisticsCommand(UnixDateTime startTime, UnixDateTime endTime)
            : base(0xa2, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<UnixDateTime>(startTime));
            Arguments.Add(new Argument<UnixDateTime>(endTime));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
