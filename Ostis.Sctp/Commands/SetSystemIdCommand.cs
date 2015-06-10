using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка системного идентификатора SC-элемента.
    /// </summary>
    public class SetSystemIdCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">адрес SC-эелемента</param>
        /// <param name="identifier">идентификатор</param>
        public SetSystemIdCommand(ScAddress address, Identifier identifier)
            : base(0xa1, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(address));
            Arguments.Add(new Argument<uint>((uint) identifier.BytesStream.Length));
            Arguments.Add(new Argument<Identifier>(identifier));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
