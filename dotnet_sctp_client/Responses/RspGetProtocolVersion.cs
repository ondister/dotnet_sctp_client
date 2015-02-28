﻿using sctp_client.CallBacks;
using sctp_client.Arguments;
using System;

namespace sctp_client.Responses
{
    public class RspGetProtocolVersion:AResponse
    {
       
		Int32 _protocolversion=0;

		public Int32 ProtocolVersion
		{

			get
			{
				if (base.Header.ReturnCode == enumReturnCode.Successfull)
				{
					_protocolversion = BitConverter.ToInt32(base.BytesStream, base.Header.Leight);
				}

				return _protocolversion;
			}
		}

        public RspGetProtocolVersion(byte[] bytesstream)
            : base(bytesstream)
        {
            
        }

     
    }
}
