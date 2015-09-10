using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Поиск всех SC-ссылок с указанным содержимым.
    /// </summary>
    public class FindLinksResponse : Response
    {
        private readonly List<ScAddress> addresses;

        /// <summary>
        /// Адреса.
        /// </summary>
        public List<ScAddress> Addresses
        { get { return addresses; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public FindLinksResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                uint linksCount = BitConverter.ToUInt32(Bytes, SctpProtocol.HeaderLength);
                if (linksCount != 0)
                {
                    int offset = sizeof(uint) + SctpProtocol.HeaderLength;
                    for (uint i = 0; i < linksCount; i++)
                    {
                        var address = ScAddress.Parse(bytes, offset);
                        if (address != null)
                        {
                            addresses.Add(address);
                        }
                        offset += SctpProtocol.ScAddressLength;
                    }
                }
            }
        }
    }
}
