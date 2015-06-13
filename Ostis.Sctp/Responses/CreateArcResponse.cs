using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами..
    /// </summary>
    public class CreateArcResponse : Response
    {
        private readonly ScAddress createdArcAddress;

        /// <summary>
        /// Адрес созданной дуги.
        /// </summary>
        public ScAddress CreatedArcAddress
        { get { return createdArcAddress; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateArcResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
#warning Использовать метод Parse
                createdArcAddress.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 2);
                createdArcAddress.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
