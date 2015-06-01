using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Установка системного идентификатора SC-элемента.
    /// </summary>
    public class SetSystemIdResponse : Response
    {
        private bool isSuccessfull;

        /// <summary>
        /// Успешно.
        /// </summary>
        public bool IsSuccesfull
        {
            get
            {
                isSuccessfull = Header.ReturnCode == ReturnCode.Successfull;
                return isSuccessfull;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public SetSystemIdResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
