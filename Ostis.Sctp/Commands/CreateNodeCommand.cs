using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class CreateNodeCommand : Command
    {
        public CreateNodeCommand(ElementType nodeType)
            : base(0x04, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ElementType>(nodeType));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
#warning Header.ArgumentsSize должен быть по-хорошему автовычислимым свойством.
        }
    }
}
