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

            Response response = new UnknownResponse(bytes);
            switch (code)
            {
                case 0x00:
                    response = new UnknownResponse(bytes);
                    break;
                case 0x01:
                    response = new CheckElementResponse(bytes);
                    break;
                case 0x02:
                    response = new GetElementTypeResponse(bytes);
                    break;
                case 0x03:
                    response = new DeleteElementResponse(bytes);
                    break;
                case 0x04:
                    response = new CreateNodeResponse(bytes);
                    break;
                case 0x05:
                    response = new CreateLinkResponse(bytes);
                    break;
                case 0x06:
                    response = new CreateArcResponse(bytes);
                    break;
                case 0x07:
                    response = new GetArcResponse(bytes);
                    break;
                case 0x08:
                    //TODO: reserved
                    break;
                case 0x09:
                    response = new GetLinkContentResponse(bytes);
                    break;
                case 0x0A:
                    response = new FindLinksResponse(bytes);
                    break;
                case 0x0B:
                    response = new SetLinkContentResponse(bytes);
                    break;
                case 0x0C:
                    response = new IterateElementsResponse(bytes);
                    break;
                case 0x0E:
                    response = new CreateSubscriptionResponse(bytes);
                    break;
                case 0x0F:
                    response = new DeleteSubscriptionResponse(bytes);
                    break;
                case 0x10:
                    response = new EmitEventsResponse(bytes);
                    break;
                case 0xA0:
                    response = new FindElementResponse(bytes);
                    break;
                case 0xA1:
                    response = new SetSystemIdResponse(bytes);
                    break;
                case 0xA2:
                    response = new GetStatisticsResponse(bytes);
                    break;
                case 0xFE:
                    response = new UnknownResponse(bytes);
                    break;
                default:
                    response = new UnknownResponse(bytes);
                    break;
            }
#warning Заменить лишнюю переменную-результат на return.
            return response;
        }
    }
}
