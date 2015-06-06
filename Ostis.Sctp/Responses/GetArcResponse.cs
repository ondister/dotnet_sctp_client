using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

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
                beginElementAddress.Offset = BitConverter.ToUInt16(bytes, Header.Length + 2);
                beginElementAddress.Segment = BitConverter.ToUInt16(bytes, Header.Length);
                endElementAddress.Offset = BitConverter.ToUInt16(bytes, Header.Length + 6);
                endElementAddress.Segment = BitConverter.ToUInt16(bytes, Header.Length + 4);
            }
        }
    }
}
