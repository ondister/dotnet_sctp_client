using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение содержимого SC-ссылки.
    /// </summary>
    public class GetLinkContentCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес ссылки для получения содержимого</param>
        public GetLinkContentCommand(ScAddress address)
            : base(0x09, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(address));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
