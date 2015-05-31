using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class GetStatisticsResponse : Response
    {
        private UInt32 timeChecksCount;
        private readonly List<StatisticData> statisticsDataList;

        public List<StatisticData> StatisticsDataList
        { get { return statisticsDataList; } }

        public uint TimeChecksCount
        {
            get 
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    timeChecksCount  = BitConverter.ToUInt32(BytesStream, Header.Length);
                }
                return timeChecksCount;
            }
        }

        public GetStatisticsResponse(byte[] bytes)
            : base(bytes)
        {
            statisticsDataList = new List<StatisticData>();
            if (TimeChecksCount != 0)
            {
                int beginindex = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                const int statisticsDataLength = 89;
                for (int statscount = 0; statscount < TimeChecksCount; statscount++)
                {
                    var statisticsData = StatisticData.GetFromBytes(bytes, beginindex);
                    statisticsDataList.Add(statisticsData);
                    beginindex += statisticsDataLength;

                }
            }
        }
    }
}
