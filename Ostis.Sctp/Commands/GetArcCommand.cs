using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение начального элемента SC-дуги.
    /// </summary>
    public class GetArcCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="arcAddress">SC-адрес дуги у которой необходимо получить начальный элемент</param>
        public GetArcCommand(ScAddress arcAddress)
            : base(0x07, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(arcAddress));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
