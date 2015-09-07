using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск SC-элемента по его системному идентификатору.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="FindElementCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="FindElement" lang="C#" />
    /// </example>
    public class FindElementCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Identifier Identifier
        { get { return identifier; } }

        private readonly Identifier identifier;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="identifier">идентификатор</param>
        public FindElementCommand(Identifier identifier)
            : base(CommandCode.FindElement)
        {
            Arguments.Add(this.identifier = identifier);
        }
    }
}
