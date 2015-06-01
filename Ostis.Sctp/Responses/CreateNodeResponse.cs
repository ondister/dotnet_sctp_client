using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Создание нового SC-узла указанного типа.
    /// </summary>
    public class CreateNodeResponse : Response
    {
        private ScAddress address;

        /// <summary>
        /// Адрес созданного узла.
        /// </summary>
        public ScAddress CreatedNodeAddress
        {
            get
            {
                if (base.Header.ReturnCode == ReturnCode.Successfull)
                {
                   
                    address.Offset = BitConverter.ToUInt16(Bytes, Header.Length + 2);
                    address.Segment = BitConverter.ToUInt16(Bytes, Header.Length);
                }

                return address;
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public CreateNodeResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
