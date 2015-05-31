using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду FindLinksCommand.
    /// </summary>
    public class FindLinksResponse : Response
    {
        private readonly UInt32 linksCount;
        private readonly List<ScAddress> addresses;

        /// <summary>
        /// Адреса.
        /// </summary>
        public List<ScAddress> ScAddresses
        {
            get 
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    if (LinksCount != 0)
                    {
                        int beginIndex = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                        const int scAddressLength = 4;
                        for (int addrcount = 0; addrcount < LinksCount; addrcount++)
                        {
                            var a = ScAddress.GetFromBytes(Bytes, beginIndex);
                            addresses.Add(a);
                            beginIndex += scAddressLength;
                        }
                    }
                    return addresses;
                }
                return addresses; 
            }
        }

        /// <summary>
        /// Количество ссылок.
        /// </summary>
#warning Проверить, имеет ли смысл.
        public UInt32 LinksCount
        { get { return linksCount; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public FindLinksResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            linksCount = Header.ReturnCode == ReturnCode.Successfull ? BitConverter.ToUInt32(Bytes, Header.Length) : 0;
        }
    }
}
