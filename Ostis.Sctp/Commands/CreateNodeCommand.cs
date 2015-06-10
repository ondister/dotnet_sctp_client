using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание нового SC-узла указанного типа.
    /// </summary>
    public class CreateNodeCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="nodeType">тип создаваемого SC-узла</param>
        public CreateNodeCommand(ElementType nodeType)
            : base(CommandCode.CreateNode, 0)
        {
            uint argumentsSize = 0;
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
