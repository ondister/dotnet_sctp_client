using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    internal class CreateArcCommand : Command
    {
#warning Во всех командах в базовый конструктор должен передаваться enum, а не числовой код!
        public CreateArcCommand(ElementType arcType, ScAddress beginAddress, ScAddress endAddress)
            : base(0x06, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ElementType>(arcType));
            Arguments.Add(new Argument<ScAddress>(beginAddress));
            Arguments.Add(new Argument<ScAddress>(endAddress));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
