using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

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
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public FindLinksResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                uint linksCount = BitConverter.ToUInt32(Bytes, Header.Length);
                if (linksCount != 0)
                {
                    int beginIndex = sizeof(uint) + Header.Length;
                    for (int a = 0; a < linksCount; a++)
                    {
                        addresses.Add(ScAddress.Parse(bytes, beginIndex));
                        beginIndex += SctpProtocol.ScAddressLength;
                    }
                }
            }
        }
    }
}
