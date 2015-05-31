using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду FindElementCommand.
    /// </summary>
    public class FindElementResponse : Response
    {
        private ScAddress address;
        private bool isFound;

        /// <summary>
        /// Элемент найден.
        /// </summary>
        public bool IsFound
        {
            get
            {
                isFound = Header.ReturnSize != 0;
                return isFound;
            }
        }

        /// <summary>
        /// Адрес.
        /// </summary>
#warning Вероятно, предыдущее свойство не имеет смысла.
        public ScAddress FoundAddress
        {
            get
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
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
        public FindElementResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
