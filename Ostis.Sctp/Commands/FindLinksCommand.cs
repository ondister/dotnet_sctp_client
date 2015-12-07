using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск всех SC-ссылок с указанным содержимым.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="FindLinksCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="FindLinks" lang="C#" />
    /// </example>
    public class FindLinksCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Содержимое для поиска.
        /// </summary>
        public LinkContent Content
        { get { return content; } }

        private readonly LinkContent content;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="content">содержимое для поиска</param>
        public FindLinksCommand(LinkContent content)
            : base(CommandCode.FindLinks)
        {
            Arguments.Add(new LinkContent(content.Data.Length));//это число байт контента
            Arguments.Add(this.content = content);
        }
    }
}
