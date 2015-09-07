namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <example>
        /// Следующий пример демонстрирует использование класса <see cref="CreateLinkCommand"/>
        /// <code source="..\Ostis.Tests\CommandsTest.cs" region="CreateLink" lang="C#" />
        /// </example>
        public CreateLinkCommand()
            : base(CommandCode.CreateLink)
        { }
    }
}
