using System;
using System.Collections.Generic;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
    /// </summary>
    public class IterateElementsResponse : Response
    {
        private readonly UInt32 constructionsCount;
        private List<ScAddress> addresses;
        private List<Construction> constructions;

        /// <summary>
        /// Получение списка конструкций.
        /// </summary>
        /// <returns>список</returns>
        public List<Construction> GetConstructions()
        {
            constructions = new List<Construction>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                int addressesCount = (Bytes.Length - Header.Length - 4) / 4;
                int addressesInConstruction = (int) constructionsCount == 0 ? 0 : addressesCount / (int)constructionsCount;

                int offset = sizeof(UInt32) + Header.Length;
#warning Вынести в более глобальную константу
                const int scAddressLength = 4;

                for (uint c = 0; c < constructionsCount; c++)
                {
                    var construction = new Construction();
                    for (int a = 0; a < addressesInConstruction; a++)
                    {
                        var address = ScAddress.GetFromBytes(Bytes, offset);
                        construction.AddAddress(address);
                        offset += scAddressLength;
                    }
                    constructions.Add(construction);
                }
            }
            return constructions;
        }

        /// <summary>
        /// Количество конструкций в списке.
        /// </summary>
#warning Не нужно.
        public UInt32 ConstructionsCount
        { get { return constructionsCount; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public IterateElementsResponse(byte[] bytes)
            : base(bytes)
        {
            addresses = new List<ScAddress>();
            constructionsCount = Header.ReturnCode == ReturnCode.Successfull ? BitConverter.ToUInt32(Bytes, Header.Length) : 0;
        }
    }
}
