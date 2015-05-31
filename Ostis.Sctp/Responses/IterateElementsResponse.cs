using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class IterateElementsResponse : Response
    {
        private readonly UInt32 constructionsCount;
        private List<ScAddress> addresses;
        private List<Construction> constructions;

        public List<Construction> GetConstructions()
        {
            constructions = new List<Construction>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                int addressesCount = (BytesStream.Length - Header.Length - 4) / 4;
                int addressesInConstruction = (int) constructionsCount == 0 ? 0 : addressesCount / (int)constructionsCount;

                int offset = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                const int scAddressLength = 4;

                for (uint c = 0; c < constructionsCount; c++)
                {
                    var construction = new Construction();
                    for (int a = 0; a < addressesInConstruction; a++)
                    {
                        var address = ScAddress.GetFromBytes(BytesStream, offset);
                        construction.AddAddress(address);
                        offset += scAddressLength;
                    }
                    constructions.Add(construction);
                }
            }
            return constructions;
        }

        public UInt32 ConstructionsCount
        { get { return constructionsCount; } }

        public IterateElementsResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            constructionsCount = Header.ReturnCode == ReturnCode.Successfull ? BitConverter.ToUInt32(BytesStream, Header.Length) : 0;
        }
    }
}
