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
            : base(CommandCode.GetStatistics, 0)
        {
            Arguments.Add(startTime);
            Arguments.Add(endTime);
        }
    }
}
