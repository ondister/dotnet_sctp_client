using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;
using System.Collections.Generic;

namespace sctp_client.Responses
{
    public class RspFindLinks:AResponse
    {
        private UInt32 _linkscount=0;
        private List<ScAddress> _scaddresses;
      
        public List<ScAddress> ScAddresses
        {
            get 
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                    if (this.LinksCount!= 0)
                    {
                       int beginindex = sizeof(UInt32) + base.Header.Leight;
                        int scaddresslength = 4;
                        for (int addrcount = 0; addrcount < this.LinksCount; addrcount++)
                        {
                            ScAddress tmpadr = ScAddress.GetFromBytes(base.BytesStream, beginindex);
                            _scaddresses.Add(tmpadr);
                            beginindex += scaddresslength;

                        }
                    }

                    return _scaddresses;
                }

                return _scaddresses; 
            }
        }
        public UInt32 LinksCount
        {
            get
            {
                
                return _linkscount;
            }
        }


        public RspFindLinks(byte[] bytesstream)
            : base(bytesstream)
        {
            _scaddresses = new List<ScAddress>();
         
            if (base.Header.ReturnCode == enumReturnCode.Successfull)
            {
                _linkscount = BitConverter.ToUInt32(base.BytesStream, base.Header.Leight);
              
            }
            else
            {
                _linkscount = 0;
            }



        }

     
    }
}
