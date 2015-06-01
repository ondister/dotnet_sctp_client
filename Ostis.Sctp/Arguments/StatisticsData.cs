using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Статистика сервера для временной метки.
    /// </summary>
    public struct StatisticsData
    {
        private readonly DateTime time;
        private readonly UInt64 nodeCount;
        private readonly UInt64 arcCount;
        private readonly UInt64 linksCount;
        private readonly UInt64 liveNodeCount;
        private readonly UInt64 liveArcCount;
        private readonly UInt64 liveLinkCount;
        private readonly UInt64 emptyCount;
        private readonly UInt64 connectionsCount;
        private readonly UInt64 commandsCount;
        private readonly UInt64 commandErrorsCount;
        private readonly bool isInitStat;

        /// <summary>
        /// Время временной метки.
        /// </summary>
        public DateTime Time
        { get { return time; } }

        /// <summary>
        /// Общее количество SC-узлов, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public UInt64 NodeCount
        { get { return nodeCount; } }

        /// <summary>
        /// Общее количество SC-дуг, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public UInt64 ArcCount
        { get { return arcCount; } }

        /// <summary>
        /// Общее количество SC-ссылок, которые есть в sc-памяти (включаея помеченные на удаление).
        /// </summary>
        public UInt64 LinksCount
        { get { return linksCount; } }

        /// <summary>
        /// Количество SC-узлов, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveNodeCount
        { get { return liveNodeCount; } }

        /// <summary>
        /// Количество SC-дуг, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveArcCount
        { get { return liveArcCount; } }

        /// <summary>
        /// Количество SC-ссылок, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveLinkCount
        { get { return liveLinkCount; } }

        /// <summary>
        ///  Количество пустых ячеек в SC-памяти.
        /// </summary>
        public UInt64 EmptyCount
        { get { return emptyCount; } }

        /// <summary>
        /// Общее количество подключений клиентов к SCTP-серверу (не активных, а общее число включая и завершенные).
        /// </summary>
        public UInt64 ConnectionsCount
        { get { return connectionsCount; } }

        /// <summary>
        /// Количество обработанных SCTP-команд (включая обработанные с ошибками).
        /// </summary>
        public UInt64 CommandsCount
        { get { return commandsCount; } }

        /// <summary>
        /// Количество обработанных с ошибками SCTP-команд.
        /// </summary>
        public UInt64 CommandErrorsCount
        { get { return commandErrorsCount; } }

        /// <summary>
        /// Флаг начального сбора статистики.
        /// Другими словами, если это значение равно <b>true</b>, то статистика была собрана при запуске SCTP-сервера.
        /// Если значение равно <b>false</b>, то статистика собрана уже во время работы сервера.
        /// </summary>
        public bool IsInitStat
        { get { return isInitStat; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        public StatisticsData(byte[] bytes, int offset)
        {
#warning Magic number. SctpProtocol.StatisticsDataLength?
            if (bytes.Length >= sizeof(UInt64) * 11 + 1 + offset)
            {
                time = UnixDateTime.ToDateTime(BitConverter.ToUInt64(bytes, sizeof(UInt64) * 0 + offset));
                nodeCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 1 + offset);
                arcCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 2 + offset);
                linksCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 3 + offset);
                liveNodeCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 4 + offset);
                liveArcCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 5 + offset);
                liveLinkCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 6 + offset);
                emptyCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 7 + offset);
                connectionsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 8 + offset);
                commandsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 9 + offset);
                commandErrorsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 10 + offset);
                isInitStat = bytes[sizeof(UInt64) * 11 + offset] > 0;
            }
            else
            {
                time = DateTime.MinValue;
                nodeCount = UInt64.MinValue;
                arcCount = UInt64.MinValue;
                linksCount = UInt64.MinValue;
                liveNodeCount = UInt64.MinValue;
                liveArcCount = UInt64.MinValue;
                liveLinkCount = UInt64.MinValue;
                emptyCount = UInt64.MinValue;
                connectionsCount = UInt64.MinValue;
                commandsCount = UInt64.MinValue;
                commandErrorsCount = UInt64.MinValue;
                isInitStat = false;
            }
        }
    }
}
