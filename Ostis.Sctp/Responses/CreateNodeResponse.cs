using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание нового SC-узла указанного типа.
    /// </summary>
    public class CreateNodeResponse : Response
    {
        private readonly ScAddress createdNodeAddress = ScAddress.Invalid;

        /// <summary>
        /// Адрес созданного узла.
        /// </summary>
        public ScAddress CreatedNodeAddress
        { get { return createdNodeAddress; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateNodeResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                createdNodeAddress = ScAddress.Parse(bytes, SctpProtocol.HeaderLength);
            }
        }
    }
}
