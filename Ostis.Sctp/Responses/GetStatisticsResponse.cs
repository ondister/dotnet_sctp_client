using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение статистики с сервера, во временных границах.
    /// </summary>
    public class GetStatisticsResponse : Response
    {
        private readonly int timeChecksCount;
        private readonly List<StatisticsData> statisticsDataList;

        /// <summary>
        /// Статистическая информация.
        /// </summary>
        public List<StatisticsData> StatisticsDataList
        { get { return statisticsDataList; } }

        /// <summary>
        /// Количество проверок времени.
        /// </summary>
        public int TimeChecksCount
        { get { return timeChecksCount; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetStatisticsResponse(byte[] bytes)
            : base(bytes)
        {
            statisticsDataList = new List<StatisticsData>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                timeChecksCount = BitConverter.ToInt32(Bytes, SctpProtocol.HeaderLength);
            }
            if (TimeChecksCount != 0)
            {
                int beginIndex = sizeof(uint) + SctpProtocol.HeaderLength;
                for (int statscount = 0; statscount < TimeChecksCount; statscount++)
                {
                    statisticsDataList.Add(new StatisticsData(bytes, beginIndex));
                    beginIndex += SctpProtocol.StatisticsDataLength;
                }
            }
        }
    }
}
