using sctp_client.CallBacks;

namespace sctp_client.Responses
{
    public class RspCheckElement:AResponse
    {
        private bool _elementisexist=false;

        public bool ElementIsExist
        {
            get {
                if (base.Header.ReturnCode == enumReturnCode.Successfull)
                {
                    _elementisexist = true;
                }
                else 
                {
                    _elementisexist = false;
                }
                return _elementisexist;
               }
        }

        public RspCheckElement(byte[] bytesstream):base(bytesstream)
        {
            
        }

     
    }
}
