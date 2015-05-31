using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду GetArcCommand.
    /// </summary>
    public class GetArcResponse : Response
    {
        private ScAddress beginElementAddress;
        private ScAddress endElementAddress;

        /// <summary>
        /// Адрес начального элемента.
        /// </summary>
        public ScAddress BeginElementAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    beginElementAddress.Offset = BitConverter.ToUInt16(Bytes, Header.Length + 2);
                    beginElementAddress.Segment = BitConverter.ToUInt16(Bytes, Header.Length);
                }
                return beginElementAddress;
            }
        }

        /// <summary>
        /// Адрес конечного элемента.
        /// </summary>
		public ScAddress EndElementAddress
		{
			get
			{
				if (Header.ReturnCode == ReturnCode.Successfull)
				{
                    endElementAddress.Offset = BitConverter.ToUInt16(Bytes, Header.Length + 6);
                    endElementAddress.Segment = BitConverter.ToUInt16(Bytes, Header.Length + 4);
				}
                return endElementAddress;
			}
		}

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetArcResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
