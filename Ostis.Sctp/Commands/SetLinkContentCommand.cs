using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка содержимого SC-ссылки.
    /// </summary>
    public class SetLinkContentCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="linkAddress">SC-адрес ссылки</param>
        /// <param name="content">данные устанавливаемого содержимого</param>
        public SetLinkContentCommand(ScAddress linkAddress, LinkContent content)
            : base(0x0b, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(linkAddress));
            Arguments.Add(new Argument<UInt32>((uint) content.Bytes.Length));
            Arguments.Add(new Argument<LinkContent>(content));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
