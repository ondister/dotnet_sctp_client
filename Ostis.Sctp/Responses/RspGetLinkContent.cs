using System;

namespace Ostis.Sctp.Responses
{
    public class GetLinkContentResponse : Response
    {
        private readonly byte[] linkContent;

        public byte[] LinkContent
        { get { return linkContent; } }

        public GetLinkContentResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnSize != 0)
            {
                linkContent = new byte[Header.ReturnSize];
                Array.Copy(BytesStream, Header.Length, linkContent, 0, linkContent.Length);
            }
            else
            {
                linkContent = new byte[0];
            }
        }
    }
}
