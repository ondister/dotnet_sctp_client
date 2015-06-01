using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Удаление SC-элемента с указанным SC-адресом.
    /// </summary>
    public class DeleteElementResponse : Response
    {
        private bool isDeleted;

        /// <summary>
        /// Было ли удалено.
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                isDeleted = Header.ReturnCode == ReturnCode.Successfull;
                return isDeleted;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public DeleteElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
