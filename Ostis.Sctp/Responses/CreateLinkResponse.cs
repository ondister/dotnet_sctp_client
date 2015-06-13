﻿using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkResponse : Response
    {
        private readonly ScAddress createdLinkAddress;

        /// <summary>
        /// Адрес созданной ссылки.
        /// </summary>
        public ScAddress CreatedLinkAddress
        { get { return createdLinkAddress; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateLinkResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
#warning Использовать метод Parse
                createdLinkAddress.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 2);
                createdLinkAddress.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
