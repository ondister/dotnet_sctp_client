using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Статистика сервера для временной метки.
    /// </summary>
    /// /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="StatisticsData"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="GetStatistics" lang="C#" />
    /// </example>
   public class StatisticsData
    {
        private readonly DateTime time;
        private readonly ulong nodeCount;
        private readonly ulong arcCount;
        private readonly ulong linksCount;
        private readonly ulong liveNodeCount;
        private readonly ulong liveArcCount;
        private readonly ulong liveLinkCount;
        private readonly ulong emptyCount;
        private readonly ulong connectionsCount;
        private readonly ulong commandsCount;
        private readonly ulong commandErrorsCount;
        private readonly bool isInitStat;

        /// <summary>
        /// Время временной метки.
        /// </summary>
        public DateTime Time
        { get { return time; } }

        /// <summary>
        /// Общее количество SC-узлов, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public ulong NodeCount
        { get { return nodeCount; } }

        /// <summary>
        /// Общее количество SC-дуг, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public ulong ArcCount
        { get { return arcCount; } }

        /// <summary>
        /// Общее количество SC-ссылок, которые есть в sc-памяти (включаея помеченные на удаление).
        /// </summary>
        public ulong LinksCount
        { get { return linksCount; } }

        /// <summary>
        /// Количество SC-узлов, которые не помечены на удаление.
        /// </summary>
        public ulong LiveNodeCount
        { get { return liveNodeCount; } }

        /// <summary>
        /// Количество SC-дуг, которые не помечены на удаление.
        /// </summary>
        public ulong LiveArcCount
        { get { return liveArcCount; } }

        /// <summary>
        /// Количество SC-ссылок, которые не помечены на удаление.
        /// </summary>
        public ulong LiveLinkCount
        { get { return liveLinkCount; } }

        /// <summary>
        ///  Количество пустых ячеек в SC-памяти.
        /// </summary>
        public ulong EmptyCount
        { get { return emptyCount; } }

        /// <summary>
        /// Общее количество подключений клиентов к SCTP-серверу (не активных, а общее число включая и завершенные).
        /// </summary>
        public ulong ConnectionsCount
        { get { return connectionsCount; } }

        /// <summary>
        /// Количество обработанных SCTP-команд (включая обработанные с ошибками).
        /// </summary>
        public ulong CommandsCount
        { get { return commandsCount; } }

        /// <summary>
        /// Количество обработанных с ошибками SCTP-команд.
        /// </summary>
        public ulong CommandErrorsCount
        { get { return commandErrorsCount; } }

        /// <summary>
        /// Флаг начального сбора статистики.
        /// Другими словами, если это значение равно <b>true</b>, то статистика была собрана при запуске SCTP-сервера.
        /// Если значение равно <b>false</b>, то статистика собрана уже во время работы сервера.
        /// </summary>
        public bool IsInitStat
        { get { return isInitStat; } }

        /// <summary>
        /// Инициализирует новые данные статистики для временной метки
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        public StatisticsData(byte[] bytes, int offset)
        {
            if (bytes.Length >= SctpProtocol.StatisticsDataLength + offset)
            {
                time = UnixDateTime.ToDateTime(BitConverter.ToUInt64(bytes, sizeof(ulong) * 0 + offset));
                nodeCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 1 + offset);
                arcCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 2 + offset);
                linksCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 3 + offset);
                liveNodeCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 4 + offset);
                liveArcCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 5 + offset);
                liveLinkCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 6 + offset);
                emptyCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 7 + offset);
                connectionsCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 8 + offset);
                commandsCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 9 + offset);
                commandErrorsCount = BitConverter.ToUInt64(bytes, sizeof(ulong) * 10 + offset);
                isInitStat = bytes[sizeof(ulong) * 11 + offset] > 0;
            }
            else
            {
                time = DateTime.MinValue;
                nodeCount = ulong.MinValue;
                arcCount = ulong.MinValue;
                linksCount = ulong.MinValue;
                liveNodeCount = ulong.MinValue;
                liveArcCount = ulong.MinValue;
                liveLinkCount = ulong.MinValue;
                emptyCount = ulong.MinValue;
                connectionsCount = ulong.MinValue;
                commandsCount = ulong.MinValue;
                commandErrorsCount = ulong.MinValue;
                isInitStat = false;
            }
        }
    }
}
