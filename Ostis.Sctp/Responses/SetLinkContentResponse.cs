namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Установка содержимого SC-ссылки.
    /// </summary>
    public class SetLinkContentResponse : Response
    {
        private readonly bool contentIsSet;

        /// <summary>
        /// Признак того, что содержимое было корректно установлено.
        /// </summary>
        public bool ContentIsSet
        { get { return contentIsSet; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public SetLinkContentResponse(byte[] bytes)
            : base(bytes)
        {
            contentIsSet = Header.ReturnCode == ReturnCode.Successfull;
        }
    }
}
