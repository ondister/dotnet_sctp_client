
namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Проверка существования элемента с указанным SC-адресом.
    /// </summary>
    public class CheckElementResponse : Response
    {
        private readonly bool elementExists;

        /// <summary>
        /// Элемент существует.
        /// </summary>
        public bool ElementExists
        { get { return elementExists; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CheckElementResponse(byte[] bytes)
            : base(bytes)
        {
            elementExists = Header.ReturnCode == ReturnCode.Successfull;
        }
    }
}
