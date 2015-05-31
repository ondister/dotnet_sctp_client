using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду GetElementTypeCommand.
    /// </summary>
    public class GetElementTypeResponse : Response
    {
        private ElementType elementType = ElementType.unknown;

        /// <summary>
        /// Тип элемента.
        /// </summary>
        public ElementType ElementType
        {
            get 
            {
                if (Header.ReturnCode == ReturnCode.Successfull)
                {
                    elementType = (ElementType) BitConverter.ToUInt16(Bytes, Header.Length);
                }
                return elementType; 
            }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetElementTypeResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
