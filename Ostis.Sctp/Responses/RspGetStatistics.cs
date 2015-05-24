using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Responses
{
    public class RspGetStatistics:AResponse
    {
        private UInt32 _timecheckscount;
        private List<StatisticData> _statdata;

        public List<StatisticData> StatisticDataList
        {
            get { return _statdata; }
        }
        public uint TimeChecksCount
        {
            get 
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {

                    _timecheckscount  = BitConverter.ToUInt32(base.BytesStream, base.Header.Leight);
                }

                return _timecheckscount;
            }
        }
       
        public RspGetStatistics(byte[] bytesstream)
            : base(bytesstream)
        {
            _statdata = new List<StatisticData>();
            if (this.TimeChecksCount != 0)
            {
                int beginindex = sizeof(UInt32) + base.Header.Leight;
                int statdatalength = 89;
                for (int statscount = 0; statscount < this.TimeChecksCount; statscount++)
                {
                    StatisticData tmpdata = StatisticData.GetFromBytes(bytesstream, beginindex);
                    _statdata.Add(tmpdata);
                    beginindex += statdatalength;

                }
            }
        }

     
    }
}
