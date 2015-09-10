namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Установка системного идентификатора SC-элемента.
    /// </summary>
    public class SetSystemIdResponse : Response
    {
        private readonly bool isSuccessfull;

        /// <summary>
        /// Успешно.
        /// </summary>
        public bool IsSuccesfull
        { get { return isSuccessfull; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public SetSystemIdResponse(byte[] bytes)
            : base(bytes)
        {
            isSuccessfull = Header.ReturnCode == ReturnCode.Successfull;
        }
    }
}
