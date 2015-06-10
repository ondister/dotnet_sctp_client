﻿using System;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение версии протокола.
    /// </summary>
    public class GetProtocolVersionResponse : Response
    {
		private readonly int protocolVersion;

        /// <summary>
        /// Версия протокола сервера.
        /// </summary>
		public int ProtocolVersion
		{ get { return protocolVersion; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetProtocolVersionResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnCode == ReturnCode.Successfull)
            {
                protocolVersion = BitConverter.ToInt32(bytes, Header.Length);
            }
        }
    }
}
