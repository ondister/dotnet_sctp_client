using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск всех SC-ссылок с указанным содержимым.
    /// </summary>
    public class FindLinksCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="content">содержимое для поиска</param>
        public FindLinksCommand(LinkContent content)
            : base(0x0a, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<uint>((uint)content.Bytes.Length));
            Arguments.Add(new Argument<LinkContent>(content));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
