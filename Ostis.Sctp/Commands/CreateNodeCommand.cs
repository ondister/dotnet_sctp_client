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
            Arguments.Add(new ElementTypeArgument(nodeType));
        }
#warning Классы комманд содержат только конструктор. Надо с этим что-то делать!
    }
}
