using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Установка содержимого SC-ссылки.
    /// </summary>
    public class SetLinkContentResponse : Response
    {
        private bool contentIsSet;

        /// <summary>
        /// Признак того, что содержимое было корректно установлено.
        /// </summary>
        public bool ContentIsSet
        {
            get
            {
                contentIsSet = Header.ReturnCode == ReturnCode.Successfull;
                return contentIsSet;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public SetLinkContentResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
