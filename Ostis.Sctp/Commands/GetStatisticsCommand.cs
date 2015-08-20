using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение статистики с сервера, во временных границах.
    /// </summary>
    public class GetStatisticsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Нижняя временная граница.
        /// </summary>
        public UnixDateTime StartTime
        { get { return startTime; } }

        /// <summary>
        /// Верхняя временная граница.
        /// </summary>
        public UnixDateTime EndTime
        { get { return endTime; } }

        private readonly UnixDateTime startTime;
        private readonly UnixDateTime endTime;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="startTime">нижняя временная граница</param>
        /// <param name="endTime">верхняя временная граница</param>
        public GetStatisticsCommand(UnixDateTime startTime, UnixDateTime endTime)
            : base(CommandCode.GetStatistics)
        {
            Arguments.Add(this.startTime = startTime);
            Arguments.Add(this.endTime = endTime);
        }
    }
}
