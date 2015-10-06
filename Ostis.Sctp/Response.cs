using System;
using System.Collections.Generic;

using Ostis.Sctp.Responses;

namespace Ostis.Sctp
{
    /// <summary>
    /// Абстрактный класс ответа сервера.
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public ResponseHeader Header
        { get { return header; } }

        /// <summary>
        /// Массив байт.
        /// </summary>
        public byte[] Bytes
        { get { return bytes; } }

        private readonly ResponseHeader header;
        private readonly byte[] bytes;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="bytes">массив байт ответа</param>
        protected Response(byte[] bytes)
        {
            this.bytes = bytes;
            header = new ResponseHeader(bytes);
        }

        internal static Response GetResponse(byte[] bytes)
        {
            CommandCode code = CommandCode.Unknown;
            if (bytes.Length > 0)
            {
                code = (CommandCode) bytes[0];
            }

            Func<byte[], Response> constructor;
            return responseConstructors.TryGetValue(code, out constructor)
                ? constructor(bytes)
                : new UnknownResponse(bytes);
        }

        private static readonly Dictionary<CommandCode, Func<byte[], Response>> responseConstructors = new Dictionary<CommandCode, Func<byte[], Response>>
        {
            { CommandCode.CheckElement, bytes => new CheckElementResponse(bytes) },
            { CommandCode.GetElementType, bytes => new GetElementTypeResponse(bytes) },
            { CommandCode.DeleteElement, bytes => new DeleteElementResponse(bytes) },
            { CommandCode.CreateNode, bytes => new CreateNodeResponse(bytes) },
            { CommandCode.CreateLink, bytes => new CreateLinkResponse(bytes) },
            { CommandCode.CreateArc, bytes => new CreateArcResponse(bytes) },
            { CommandCode.GetArc, bytes => new GetArcElementsResponse(bytes) },
            { CommandCode.GetLinkContent, bytes => new GetLinkContentResponse(bytes) },
            { CommandCode.FindLinks, bytes => new FindLinksResponse(bytes) },
            { CommandCode.SetLinkContent, bytes => new SetLinkContentResponse(bytes) },
            { CommandCode.IterateElements, bytes => new IterateElementsResponse(bytes) },
            { CommandCode.IterateConstructions, bytes => new IterateConstructionsResponse(bytes) },
            { CommandCode.CreateSubscription, bytes => new CreateSubscriptionResponse(bytes) },
            { CommandCode.DeleteSubscription, bytes => new DeleteSubscriptionResponse(bytes) },
            { CommandCode.EmitEvents, bytes => new EmitEventsResponse(bytes) },
            { CommandCode.FindElement, bytes => new FindElementResponse(bytes) },
            { CommandCode.SetSystemId, bytes => new SetSystemIdResponse(bytes) },
            { CommandCode.GetStatistics, bytes => new GetStatisticsResponse(bytes) },
        };
    }
}
