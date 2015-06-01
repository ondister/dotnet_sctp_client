using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами..
    /// </summary>
    public class CreateArcResponse : Response
    {
#warning Это поле - явно лишнее.
        private ScAddress address;

        /// <summary>
        /// Адрес созданной дуги.
        /// </summary>
        public ScAddress CreatedArcAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    address.Offset = BitConverter.ToUInt16(Bytes, Header.Length + 2);
                    address.Segment = BitConverter.ToUInt16(Bytes, Header.Length);
                }
                return address;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateArcResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
