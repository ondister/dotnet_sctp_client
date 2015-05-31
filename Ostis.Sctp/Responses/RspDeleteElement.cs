using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class DeleteElementResponse : Response
    {
        private bool isDeleted;

        public bool IsDeleted
        {
            get
            {
                isDeleted = Header.ReturnCode == ReturnCode.Successfull;
                return isDeleted;
            }
        }

        public DeleteElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
