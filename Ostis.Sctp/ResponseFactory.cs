using Ostis.Sctp.Responses;

namespace Ostis.Sctp
{
    internal class ResponseFactory
    {
        public Response GetResponse(byte[] bytes)
        {
            byte code = 0;
            if (bytes.Length != 0)
            {
                code = bytes[0];
            }

#warning Заменить числовые коды на enum.
            switch (code)
            {
                case 0x00:
                    return new UnknownResponse(bytes);
                case 0x01:
                    return new CheckElementResponse(bytes);
                case 0x02:
                    return new GetElementTypeResponse(bytes);
                case 0x03:
                    return new DeleteElementResponse(bytes);
                case 0x04:
                    return new CreateNodeResponse(bytes);
                case 0x05:
                    return new CreateLinkResponse(bytes);
                case 0x06:
                    return new CreateArcResponse(bytes);
                case 0x07:
                    return new GetArcResponse(bytes);
                //case 0x08:
                    // TODO: reserved
                case 0x09:
                    return new GetLinkContentResponse(bytes);
                case 0x0A:
                    return new FindLinksResponse(bytes);
                case 0x0B:
                    return new SetLinkContentResponse(bytes);
                case 0x0C:
                    return new IterateElementsResponse(bytes);
                case 0x0E:
                    return new CreateSubscriptionResponse(bytes);
                case 0x0F:
                    return new DeleteSubscriptionResponse(bytes);
                case 0x10:
                    return new EmitEventsResponse(bytes);
                case 0xA0:
                    return new FindElementResponse(bytes);
                case 0xA1:
                    return new SetSystemIdResponse(bytes);
                case 0xA2:
                    return new GetStatisticsResponse(bytes);
                case 0xFE:
                    return new UnknownResponse(bytes);
                default:
                    return new UnknownResponse(bytes);
            }
        }
    }
}
