using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание нового SC-узла указанного типа.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="CreateNodeCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="CreateNode" lang="C#" />
    /// </example>
    public class CreateNodeCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Тип создаваемого SC-узла.
        /// </summary>
        public ElementType NodeType
        { get { return nodeType; } }

        private readonly ElementType nodeType;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="nodeType">тип создаваемого SC-узла</param>
        public CreateNodeCommand(ElementType nodeType)
            : base(CommandCode.CreateNode)
        {
            Arguments.Add(new ElementTypeArgument(this.nodeType = nodeType));
        }
    }
}
