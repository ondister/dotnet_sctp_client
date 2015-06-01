using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение статистики с сервера, во временных границах.
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
                for (int statscount = 0; statscount < TimeChecksCount; statscount++)
                {
                    statisticsDataList.Add(new StatisticsData(bytes, beginindex));
                    beginindex += SctpProtocol.StatisticsDataLength;
                }
            }
        }
    }
}
