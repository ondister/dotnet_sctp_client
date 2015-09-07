using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
    /// </summary>
    /// <remarks>
    /// Виды используемых шаблонов с пояснениями смотрите в классе <see cref="T:Ostis.Sctp.Arguments.ConstructionTemplate"/>
    /// </remarks>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="IterateElementsCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="IterateElements" lang="C#" />
    /// </example>
    public class IterateElementsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Шаблон для поиска.
        /// </summary>
        public ConstructionTemplate Template
        { get { return template; } }

        private readonly ConstructionTemplate template;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="template">шаблон для поиска</param>
        public IterateElementsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateElements)
        {
            Arguments.Add(this.template = template);
        }
    }
}
