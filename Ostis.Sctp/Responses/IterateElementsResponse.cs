using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
    /// </summary>
    public class IterateElementsResponse : Response
    {
        private readonly List<List<ScAddress>> constructions;

        /// <summary>
        /// Список конструкций.
        /// </summary>
        public List<List<ScAddress>> Constructions
        { get { return constructions; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public IterateElementsResponse(byte[] bytes)
            : base(bytes)
        {
            constructions = new List<List<ScAddress>>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                int constructionsCount = BitConverter.ToInt32(Bytes, SctpProtocol.HeaderLength);
                int addressesCount = (bytes.Length - SctpProtocol.HeaderLength - sizeof(uint)) / SctpProtocol.ScAddressLength;
                int addressesInConstruction = constructionsCount == 0 ? 0 : (addressesCount / constructionsCount);
                int offset = SctpProtocol.HeaderLength + sizeof(uint);
                for (uint c = 0; c < constructionsCount; c++)
                {
                    var construction = new List<ScAddress>();
                    for (int a = 0; a < addressesInConstruction; a++)
                    {
                        var address = ScAddress.Parse(Bytes, offset);
                        if (address != null)
                        {
                            construction.Add(address);
                        }
                        offset += SctpProtocol.ScAddressLength;
                    }
                    constructions.Add(construction);
                }
            }
        }
    }
}
