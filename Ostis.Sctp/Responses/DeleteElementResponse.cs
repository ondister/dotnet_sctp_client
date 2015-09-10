namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Удаление SC-элемента с указанным SC-адресом.
    /// </summary>
    public class DeleteElementResponse : Response
    {
        private readonly bool isDeleted;

        /// <summary>
        /// Было ли удалено.
        /// </summary>
        public bool IsDeleted
        { get { return isDeleted; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public DeleteElementResponse(byte[] bytes)
            : base(bytes)
        {
            isDeleted = Header.ReturnCode == ReturnCode.Successfull;
        }
    }
}
