using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение начального элемента SC-дуги.
    /// </summary>
    public class GetArcResponse : Response
    {
        private readonly ScAddress beginElementAddress;
        private readonly ScAddress endElementAddress;

        /// <summary>
        /// Адрес начального элемента.
        /// </summary>
        public ScAddress BeginElementAddress
        { get { return beginElementAddress; } }

        /// <summary>
        /// Адрес конечного элемента.
        /// </summary>
		public ScAddress EndElementAddress
		{ get { return endElementAddress; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetArcResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
#warning Parse Parse Size
                beginElementAddress.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 2);
                beginElementAddress.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength);
                endElementAddress.Offset = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 6);
                endElementAddress.Segment = BitConverter.ToUInt16(bytes, SctpProtocol.HeaderLength + 4);
            }
        }
    }
}
