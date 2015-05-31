using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    public class SetLinkContentResponse : Response
    {
        private bool contentIsSet;

        public bool ContentIsSet
        {
            get
            {
                contentIsSet = Header.ReturnCode == ReturnCode.Successfull;
                return contentIsSet;
            }
        }

        public SetLinkContentResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
