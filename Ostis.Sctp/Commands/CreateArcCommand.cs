using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами.
    /// </summary>
    public class CreateArcCommand : Command
    {
#warning Во всех командах в базовый конструктор должен передаваться enum, а не числовой код!
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="arcType">тип создаваемой SC-дуги</param>
        /// <param name="beginAddress">SC-адрес начального элемента SC-дуги</param>
        /// <param name="endAddress">SC-адрес конечного элемента SC-дуги</param>
        public CreateArcCommand(ElementType arcType, ScAddress beginAddress, ScAddress endAddress)
            : base(0x06, 0)
        {
            uint argumentsSize = 0;
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
