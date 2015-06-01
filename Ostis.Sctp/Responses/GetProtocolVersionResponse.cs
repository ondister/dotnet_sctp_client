using System;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение версии протокола.
    /// </summary>
    public class GetProtocolVersionResponse : Response
    {
		Int32 protocolVersion;

        /// <summary>
        /// Версия протокола сервера.
        /// </summary>
		public Int32 ProtocolVersion
		{
			get
			{
				if (Header.ReturnCode == ReturnCode.Successfull)
				{
					protocolVersion = BitConverter.ToInt32(Bytes, Header.Length);
				}
				return protocolVersion;
			}
		}

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetProtocolVersionResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
