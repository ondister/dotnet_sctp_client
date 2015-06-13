﻿using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Поиск SC-элемента по его системному идентификатору.
    /// </summary>
    public class FindElementResponse : Response
    {
        private readonly ScAddress address;
        private readonly bool isFound;

        /// <summary>
        /// Элемент найден.
        /// </summary>
        public bool IsFound
        { get { return isFound; } }

        /// <summary>
        /// Адрес.
        /// </summary>
#warning Вероятно, предыдущее свойство не имеет смысла.
        public ScAddress FoundAddress
        { get { return address; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public FindElementResponse(byte[] bytes)
            : base(bytes)
        {
            isFound = Header.ReturnSize != 0;
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
#warning Parse
                address.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 2);
                address.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
