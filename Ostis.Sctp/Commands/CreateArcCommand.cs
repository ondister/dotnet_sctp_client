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
            : base(CommandCode.CreateArc)
        {
            Arguments.Add(new ElementTypeArgument(arcType));
            Arguments.Add(beginAddress);
            Arguments.Add(endAddress);
        }
    }
}
