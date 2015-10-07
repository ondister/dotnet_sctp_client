using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами..
    /// </summary>
    public class CreateArcResponse : Response
    {
        private readonly ScAddress createdArcAddress=ScAddress.Invalid;

        /// <summary>
        /// Адрес созданной дуги.
        /// </summary>
        public ScAddress CreatedArcAddress
        { get { return createdArcAddress; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateArcResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                createdArcAddress = ScAddress.Parse(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
