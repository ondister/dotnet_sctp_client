using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Запрос всех произошедших событий.
    /// </summary>
    public class EmitEventsResponse : Response
    {
        private readonly List<ScEvent> events;

        /// <summary>
        /// События.
        /// </summary>
        public List<ScEvent> ScEvents
        { get { return events; } }

        //        public List<ScAddress> ScAddresses
        //        {
        //            get 
        //            {
        //                if (base.Header.ReturnCode == enumReturnCode.Successfull)
        //                {
        //                    if (this.LinksCount!= 0)
        //                    {
        //                       int beginindex = sizeof(uint) + base.Header.Leight;
        //                        int scaddresslength = 4;
        //                        for (int addrcount = 0; addrcount < this.LinksCount; addrcount++)
        //                        {
        //                            ScAddress tmpadr = ScAddress.GetFromBytes(base.BytesStream, beginindex);
        //                            _scaddresses.Add(tmpadr);
        //                            beginindex += scaddresslength;
        //
        //                        }
        //                    }
        //
        //                    return _scaddresses;
        //                }
        //
        //                return _scaddresses; 
        //            }
        //        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public EmitEventsResponse(byte[] bytes)
            : base(bytes)
        {
            events = new List<ScEvent>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                uint eventsCount = BitConverter.ToUInt32(bytes, Header.Length);
                if (eventsCount > 0)
                {
                    int beginIndex = sizeof(uint) + Header.Length;
                    for (int e = 0; e < eventsCount; e++)
                    {
                        events.Add(ScEvent.GetFromBytes(bytes, beginIndex));
                        beginIndex += SctpProtocol.ScEventLength;
                    }
                }
            }
        }
    }
}
