using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду CheckElementCommand.
    /// </summary>
    public class CheckElementResponse : Response
    {
#warning Это поле - явно лишнее.
        private bool elementExists;

        /// <summary>
        /// Элемент существует.
        /// </summary>
        public bool ElementIsExist
        {
            get
            {
                elementExists = Header.ReturnCode == ReturnCode.Successfull;
                return elementExists;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CheckElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
