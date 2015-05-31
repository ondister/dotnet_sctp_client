using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду GetStatisticsCommand.
    /// </summary>
    public class GetStatisticsResponse : Response
    {
        private UInt32 timeChecksCount;
        private readonly List<StatisticsData> statisticsDataList;

        /// <summary>
        /// Статистическая информация.
        /// </summary>
        public List<StatisticsData> StatisticsDataList
        { get { return statisticsDataList; } }

        /// <summary>
        /// Количество проверок времени.
        /// </summary>
        public uint TimeChecksCount
        {
            get 
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    timeChecksCount  = BitConverter.ToUInt32(Bytes, Header.Length);
                }
                return timeChecksCount;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetStatisticsResponse(byte[] bytes)
            : base(bytes)
        {
            statisticsDataList = new List<StatisticsData>();
            if (TimeChecksCount != 0)
            {
                int beginindex = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                const int statisticsDataLength = 89;
                for (int statscount = 0; statscount < TimeChecksCount; statscount++)
                {
                    var statisticsData = StatisticsData.GetFromBytes(bytes, beginindex);
                    statisticsDataList.Add(statisticsData);
                    beginindex += statisticsDataLength;
                }
            }
        }
    }
}
