using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Удаление SC-элемента с указанным SC-адресом.
    /// </summary>
    public class DeleteElementCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес удаляемого sc-элемента</param>
        public DeleteElementCommand(ScAddress address)
            : base(0x03, 0)
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
