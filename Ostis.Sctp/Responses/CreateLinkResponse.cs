using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkResponse : Response
    {
        private readonly ScAddress createdLinkAddress = ScAddress.Invalid;

        /// <summary>
        /// Адрес созданной ссылки.
        /// </summary>
        public ScAddress CreatedLinkAddress
        { get { return createdLinkAddress; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateLinkResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                createdLinkAddress = ScAddress.Parse(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
