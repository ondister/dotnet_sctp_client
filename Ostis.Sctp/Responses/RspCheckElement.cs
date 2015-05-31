using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class CheckElementResponse : Response
    {
#warning Это поле - явно лишнее.
        private bool elementExists;

        public bool ElementIsExist
        {
            get
            {
                elementExists = Header.ReturnCode == ReturnCode.Successfull;
                return elementExists;
            }
        }

        public CheckElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
