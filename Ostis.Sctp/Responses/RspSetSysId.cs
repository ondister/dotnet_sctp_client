using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class SetSystemIdResponse : Response
    {
        private bool isSuccessfull;

        public bool IsSuccesfull
        {
            get
            {
                isSuccessfull = Header.ReturnCode == ReturnCode.Successfull;
                return isSuccessfull;
            }
        }

        public SetSystemIdResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
