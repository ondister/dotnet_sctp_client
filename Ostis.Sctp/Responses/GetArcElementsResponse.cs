using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение начального элемента SC-дуги.
    /// </summary>
    public class GetArcElementsResponse : Response
    {
        private readonly ScAddress beginElementAddress = ScAddress.Invalid;
        private readonly ScAddress endElementAddress = ScAddress.Invalid;

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
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetArcElementsResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                beginElementAddress = ScAddress.Parse(bytes, SctpProtocol.HeaderLength);
                endElementAddress = ScAddress.Parse(bytes, SctpProtocol.HeaderLength + SctpProtocol.ScAddressLength);
            }
        }
    }
}
