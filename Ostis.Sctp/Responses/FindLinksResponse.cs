using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class FindLinksResponse : Response
    {
        private readonly UInt32 linksCount;
        private readonly List<ScAddress> addresses;
      
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
                            var a = ScAddress.GetFromBytes(BytesStream, beginIndex);
                            addresses.Add(a);
                            beginIndex += scAddressLength;
                        }
                    }
                    return addresses;
                }
                return addresses; 
            }
        }

        public UInt32 LinksCount
        { get { return linksCount; } }

        public FindLinksResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            linksCount = Header.ReturnCode == ReturnCode.Successfull ? BitConverter.ToUInt32(BytesStream, Header.Length) : 0;
        }
    }
}
