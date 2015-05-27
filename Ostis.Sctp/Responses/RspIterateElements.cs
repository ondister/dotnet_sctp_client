using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Responses
{
    public class RspIterateElements : AResponse
    {
        private UInt32 _constrcount = 0;
        private List<ScAddress> _scaddresses;
        private List<Construction> _constructions;

        public List<Construction> GetConstructions()
        {
            _constructions = new List<Construction>();

            if (base.Header.ReturnCode == ReturnCode.Successfull)
            {
                int addrcount = (base.BytesStream.Length - base.Header.Length - 4) / 4;
                int addrinconstruction = (int)_constrcount==0?0:addrcount / (int)_constrcount;

                int offset = sizeof(UInt32) + base.Header.Length;
                int scaddresslength = 4;

                for (uint iteration = 0; iteration < _constrcount; iteration++)
                {
                    Construction tmpconstr = new Construction();
                    for (int addrit = 0; addrit < addrinconstruction; addrit++)
                    {
                        ScAddress tmpaddr =ScAddress.GetFromBytes(base.BytesStream, offset);
                        tmpconstr.AddScAddress(tmpaddr);
                        offset += scaddresslength;
                    }
                    _constructions.Add(tmpconstr);
                }


            }


            return _constructions;
        }

       

        public UInt32 ConstructionsCount
        {
            get
            {

                return _constrcount;
            }
        }


        public RspIterateElements(byte[] bytesstream)
            : base(bytesstream)
        {
            _scaddresses = new List<ScAddress>();
            if (base.Header.ReturnCode == ReturnCode.Successfull)
            {
                _constrcount = BitConverter.ToUInt32(base.BytesStream, base.Header.Length);
            }
            else
            {
                _constrcount = 0;
            }



        }


    }
}
