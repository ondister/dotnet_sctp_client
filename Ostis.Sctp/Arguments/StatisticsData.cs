using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Статистика сервера для временной метки.
    /// </summary>
    public struct StatisticsData
    {
#warning Переименовать все поля и свойства, поля станут readonly.
        private DateTime mTime; // unix time
        private UInt64 mNodeCount; // amount of all nodes
        private UInt64 mArcCount; // amount of all arcs
        private UInt64 mLinksCount; // amount of all links
        private UInt64 mLiveNodeCount; // amount of live nodes
        private UInt64 mLiveArcCount; // amount of live arcs
        private UInt64 mLiveLinkCount; // amount of live links
        private UInt64 mEmptyCount; // amount of empty sc-elements
        private UInt64 mConnectionsCount;  // amount of collected clients
        private UInt64 mCommandsCount; // amount of processed commands (it includes commands with errors)
        private UInt64 mCommandErrorsCount; // amount of command, that was processed with error
        private byte mIsInitStat;   // flag on initial stat save

        /// <summary>
        /// Время временной метки.
        /// </summary>
        public DateTime Time
        { get { return mTime; } }

        /// <summary>
        /// Общее количество SC-узлов, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public UInt64 NodeCount
        { get { return mNodeCount; } }

        /// <summary>
        /// Общее количество SC-дуг, которые есть в SC-памяти (включая помеченные на удаление).
        /// </summary>
        public UInt64 ArcCount
        { get { return mArcCount; } }

        /// <summary>
        /// Общее количество SC-ссылок, которые есть в sc-памяти (включаея помеченные на удаление).
        /// </summary>
        public UInt64 LinksCount
        { get { return mLinksCount; } }

        /// <summary>
        /// Количество SC-узлов, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveNodeCount
        { get { return mLiveNodeCount; } }

        /// <summary>
        /// Количество SC-дуг, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveArcCount
        { get { return mLiveArcCount; } }

        /// <summary>
        /// Количество SC-ссылок, которые не помечены на удаление.
        /// </summary>
        public UInt64 LiveLinkCount
        { get { return mLiveLinkCount; } }

        /// <summary>
        ///  Количество пустых ячеек в SC-памяти.
        /// </summary>
        public UInt64 EmptyCount
        { get { return mEmptyCount; } }

        /// <summary>
        /// Общее количество подключений клиентов к SCTP-серверу (не активных, а общее число включая и завершенные).
        /// </summary>
        public UInt64 ConnectionsCount
        { get { return mConnectionsCount; } }

        /// <summary>
        /// Количество обработанных SCTP-команд (включая обработанные с ошибками).
        /// </summary>
        public UInt64 CommandsCount
        { get { return mCommandsCount; } }

        /// <summary>
        /// Количество обработанных с ошибками SCTP-команд.
        /// </summary>
        public UInt64 CommandErrorsCount
        { get { return mCommandErrorsCount; } }

#warning Заменить на bool.
        /// <summary>
        /// Флаг начального сбора статистики.
        /// Другими словами, если это значание равно 1, то статистика была собрана при запуске sctp сервера.
        /// Если значание равно 0, то статистика собрана уже во время работы сервера.
        /// </summary>
        public byte IsInitStat
        { get { return mIsInitStat; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">ммещение в массиве</param>
#warning Этот метод должен превратиться в конструктор.
        public static StatisticsData GetFromBytes(byte[] bytes, int offset)
        {
            StatisticsData tmpstat = new StatisticsData();
#warning Magic number.
            if (bytes.Length >= sizeof(UInt64) * 11 + 1 + offset)
            {
                tmpstat.mTime = UnixDateTime.ToDateTime(BitConverter.ToUInt64(bytes, sizeof(UInt64) * 0 + offset));
                tmpstat.mNodeCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 1 + offset);
                tmpstat.mArcCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 2 + offset);
                tmpstat.mLinksCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 3 + offset);
                tmpstat.mLiveNodeCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 4 + offset);
                tmpstat.mLiveArcCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 5 + offset);
                tmpstat.mLiveLinkCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 6 + offset);
                tmpstat.mEmptyCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 7 + offset);
                tmpstat.mConnectionsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 8 + offset);
                tmpstat.mCommandsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 9 + offset);
                tmpstat.mCommandErrorsCount = BitConverter.ToUInt64(bytes, sizeof(UInt64) * 10 + offset);
                tmpstat.mIsInitStat = bytes[sizeof(UInt64) * 11 + offset];
            }
            else
            {
                tmpstat.mTime = DateTime.MinValue;
                tmpstat.mNodeCount = UInt64.MinValue;
                tmpstat.mArcCount = UInt64.MinValue;
                tmpstat.mLinksCount = UInt64.MinValue;
                tmpstat.mLiveNodeCount = UInt64.MinValue;
                tmpstat.mLiveArcCount = UInt64.MinValue;
                tmpstat.mLiveLinkCount = UInt64.MinValue;
                tmpstat.mEmptyCount = UInt64.MinValue;
                tmpstat.mConnectionsCount = UInt64.MinValue;
                tmpstat.mCommandsCount = UInt64.MinValue;
                tmpstat.mCommandErrorsCount = UInt64.MinValue;
                tmpstat.mIsInitStat = 0;
            }
            return tmpstat;
        }
    }
}
