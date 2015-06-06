using System;

using Ostis.Sctp.Arguments;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение типа SC-элемента по SC-адресу.
    /// </summary>
    public class GetElementTypeResponse : Response
    {
        private readonly ElementType elementType;

        /// <summary>
        /// Тип элемента.
        /// </summary>
        public ElementType ElementType
        { get { return elementType; } }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetElementTypeResponse(byte[] bytes)
            : base(bytes)
        {
            elementType = Header.ReturnCode == ReturnCode.Successfull
                ? (ElementType) BitConverter.ToUInt16(bytes, Header.Length)
                : ElementType.Unknown;
        }
    }
}
