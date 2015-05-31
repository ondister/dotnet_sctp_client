using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду EmitEventsCommand.
    /// </summary>
    public class EmitEventsResponse : Response
    {
        private readonly UInt32 eventsCount;
        private List<ScEvent> events;

        /// <summary>
        /// События.
        /// </summary>
        public List<ScEvent> ScEvents
        {
            get
            {
                events = new List<ScEvent>();
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    if (EventsCount != 0)
                    {
                        int beginIndex = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                        const int scEventLength = 12;
                        for (int e = 0; e < EventsCount; e++)
                        {
                            events.Add(ScEvent.GetFromBytes(Bytes, beginIndex));
                            beginIndex += scEventLength;
                        }
                    }
                }
                return events;
            }
        }

        //        public List<ScAddress> ScAddresses
        //        {
        //            get 
        //            {
        //                if (base.Header.ReturnCode == enumReturnCode.Successfull)
        //                {
        //                    if (this.LinksCount!= 0)
        //                    {
        //                       int beginindex = sizeof(UInt32) + base.Header.Leight;
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
        /// Количество событий.
        /// </summary>
#warning Удалить лишнее?
        public UInt32 EventsCount
        { get { return eventsCount; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public EmitEventsResponse(byte[] bytes)
            : base(bytes)
        {
            events = new List<ScEvent>();
            eventsCount = Header.ReturnCode == ReturnCode.Successfull ? BitConverter.ToUInt32(Bytes, Header.Length) : 0;
        }
    }
}
