using Ostis.Sctp.CallBacks;
using Ostis.Sctp.Arguments;
using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Responses
{
	/// <summary>
	/// Rsp events emit.
	/// </summary>
	public class RspEventsEmit:AResponse
	{
		private UInt32 _eventscount = 0;
		private List<ScEvent> _scevents;

		/// <summary>
		/// Gets the sc events.
		/// </summary>
		/// <value>The sc events.</value>
		public List<ScEvent> ScEvents {
			get {
				_scevents = new List<ScEvent> ();
				if (base.Header.ReturnCode == enumReturnCode.Successfull) {
					if (this.EventsCount != 0) {

						int beginindex = sizeof(UInt32) + base.Header.Length;
						int sceventlength = 12;
						for (int eventcount = 0; eventcount < this.EventsCount; eventcount++) {
							ScEvent tmpevent = ScEvent.GetFromBytes (base.BytesStream, beginindex);
							_scevents.Add (tmpevent);
							beginindex += sceventlength;
						
						}



					}
				}
				return _scevents;
			}
		}
		//        public List<ScAddress> ScAddresses
		//        {
		//            get 
		//            {
		//                if (base.Header.ReturnCode == enumReturnCode.Successfull)
		//                {
		//                    if (this.LinksCount!= 0)
		//                    {
		//                       int beginindex = sizeof(UInt32) + base.Header.Leight;
		//                        int scaddresslength = 4;
		//                        for (int addrcount = 0; addrcount < this.LinksCount; addrcount++)
		//                        {
		//                            ScAddress tmpadr = ScAddress.GetFromBytes(base.BytesStream, beginindex);
		//                            _scaddresses.Add(tmpadr);
		//                            beginindex += scaddresslength;
		//
		//                        }
		//                    }
		//
		//                    return _scaddresses;
		//                }
		//
		//                return _scaddresses; 
		//            }
		//        }
		public UInt32 EventsCount {
			get {
                
				return _eventscount;
			}
		}

		public RspEventsEmit (byte[] bytesstream)
            : base(bytesstream)
		{
			_scevents = new List<ScEvent> ();
         
			if (base.Header.ReturnCode == enumReturnCode.Successfull) {
				_eventscount = BitConverter.ToUInt32 (base.BytesStream, base.Header.Length);
			} else {
				_eventscount = 0;
			}



		}
	}
}
