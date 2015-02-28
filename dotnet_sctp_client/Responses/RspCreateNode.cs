﻿using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspCreateNode:AResponse
    {
        private ScAddress _address=new ScAddress();

        public ScAddress CreatedNodeAddress
        {
            get
            {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                   
                    _address.Offset = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight + 2);
                    _address.Segment = BitConverter.ToUInt16(base.BytesStream, base.Header.Leight);
                }

                return _address;
            }
        }


        public RspCreateNode(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
