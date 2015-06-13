using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;

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
                uint eventsCount = BitConverter.ToUInt32(bytes, SctpProtocol.HeaderLength);
                if (eventsCount > 0)
                {
                    int beginIndex = sizeof(uint) + SctpProtocol.HeaderLength;
                    for (uint i = 0; i < eventsCount; i++)
                    {
                        events.Add(ScEvent.Parse(bytes, beginIndex));
                        beginIndex += SctpProtocol.ScEventLength;
                    }
                }
            }
        }
    }
}
