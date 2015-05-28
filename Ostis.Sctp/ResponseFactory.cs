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

            Response response = new RspUnknown(bytes);
            switch (code)
            {
                case 0x00:
                    response = new RspUnknown(bytes);
                    break;
                case 0x01:
                    response = new RspCheckElement(bytes);
                    break;
                case 0x02:
                    response = new RspGetElementType(bytes);
                    break;
                case 0x03:
                    response = new RspDeleteElement(bytes);
                    break;
                case 0x04:
                    response = new RspCreateNode(bytes);
                    break;
                case 0x05:
                    response = new RspCreateLink(bytes);
                    break;
                case 0x06:
                    response = new RspCreateArc(bytes);
                    break;
                case 0x07:
                    response = new RspGetArc(bytes);
                    break;
                case 0x08:
                    //TODO:reserved
                    break;
                case 0x09:
                    response = new RspGetLInkContent(bytes);
                    break;
                case 0x0A:
                    response = new RspFindLinks(bytes);
                    break;
                case 0x0B:
                    response = new RspSetLinkContent(bytes);
                    break;
                case 0x0C:
                    response = new RspIterateElements(bytes);
                    break;
                case 0x0E:
                    response = new RspCreateEventSubscription(bytes);
                    break;
                case 0x0F:
                    response = new RspDeleteEventSubscription(bytes);
                    break;
                case 0x10:
                    response = new RspEventsEmit(bytes);
                    break;
                case 0xA0:
                    response = new RspFindElementById(bytes);
                    break;
                case 0xA1:
                    response = new RspSetSysId(bytes);
                    break;
                case 0xA2:
                    response = new RspGetStatistics(bytes);
                    break;
                case 0xFE:
                    response = new RspUnknown(bytes);
                    break;
                default:
                    response = new RspUnknown(bytes);
                    break;
            }
            return response;
        }
    }
}
