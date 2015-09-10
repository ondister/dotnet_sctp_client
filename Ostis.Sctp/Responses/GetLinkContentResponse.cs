using System;

namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду: Получение содержимого SC-ссылки.
    /// </summary>
    public class GetLinkContentResponse : Response
    {
        private readonly byte[] linkContent;

        /// <summary>
        /// Содержимое ссылки.
        /// </summary>
        public byte[] LinkContent
        { get { return linkContent; } }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public GetLinkContentResponse(byte[] bytes)
            : base(bytes)
        {
            if (Header.ReturnSize != 0)
            {
                linkContent = new byte[Header.ReturnSize];
                Array.Copy(bytes, SctpProtocol.HeaderLength, linkContent, 0, linkContent.Length);
            }
            else
            {
                linkContent = new byte[0];
            }
        }
    }
}
