using sctp_client.Responses;

namespace sctp_client
{
    internal class ResponseFactory
    {
        public AResponse GetResponse(byte[] bytesstream)
        {
            byte code = 0;
            if (bytesstream.Length != 0)
            {
                code = bytesstream[0];
            }
            
            AResponse response = new RspUnknown(bytesstream);
            switch (code)
            {
                case 0x00:
                    response = new RspUnknown(bytesstream);
                    break;
                case 0x01:
                    response = new RspCheckElement(bytesstream);
                    break;
                case 0x02:
                    response = new RspGetElementType(bytesstream);
                    break;
                case 0x03:
                    response = new RspDeleteElement(bytesstream);
                    break;
                case 0x04:
                    response = new RspCreateNode(bytesstream);
                    break;
                case 0x05:
                    response = new RspCreateLink(bytesstream);
                    break;
                case 0x06:
                    response = new RspCreateArc(bytesstream);
                    break;
                case 0x07:
                    response = new RspGetArc(bytesstream);
                    break;
                case 0x08:
                    //TODO:resorved
                    break;
                case 0x09:
                    response =new RspGetLInkContent(bytesstream);
                    break;
                case 0x0A:
                    response = new RspFindLinks(bytesstream);
                    break;
                case 0x0B:
                    response = new RspSetLinkContent(bytesstream);
                    break;
                case 0x0C:
                    response = new RspIterateElements(bytesstream);
                    break;
				    case 0x0E:
					response = new RspCreateEventSubscription(bytesstream);
					break;
		      	case 0x0F:
				response = new RspDeleteEventSubscription (bytesstream);
				break;
	                case 0xA0:
	                response = new RspFindElementById(bytesstream);
                    break;
                case 0xA1:
                    response = new RspSetSysId(bytesstream);
                    break;
                case 0xA2:
                    response = new RspGetStatistics(bytesstream);
                    break;
                case 0xFE:
                    response = new RspUnknown(bytesstream);
                    break;
                default:
                    response = new RspUnknown(bytesstream);
                    break;


            }
            return response;
        }
    }
}
