﻿using System;
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
        private readonly List<Construction> constructions;

        /// <summary>
        /// Список конструкций.
        /// </summary>
        public List<Construction> Constructions
        { get { return constructions; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public IterateElementsResponse(byte[] bytes)
            : base(bytes)
        {
            constructions = new List<Construction>();
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                uint constructionsCount = BitConverter.ToUInt32(Bytes, Header.Length);
                int addressesCount = (bytes.Length - Header.Length - 4) / 4;
#warning Правильно ли записано выражение после расстановки скобок согласно правилам приоритета операторов C#???
                int addressesInConstruction = ((int)constructionsCount == 0 ? 0 : addressesCount) / (int)constructionsCount;
                int offset = sizeof(uint) + Header.Length;
                for (uint c = 0; c < constructionsCount; c++)
                {
                    var construction = new Construction();
                    for (int a = 0; a < addressesInConstruction; a++)
                    {
                        var address = ScAddress.GetFromBytes(Bytes, offset);
                        construction.AddAddress(address);
                        offset += SctpProtocol.ScAddressLength;
                    }
                    constructions.Add(construction);
                }
            }
        }
    }
}
