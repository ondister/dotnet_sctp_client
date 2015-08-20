using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск всех SC-ссылок с указанным содержимым.
    /// </summary>
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
            Arguments.Add(this.content = content);
        }
    }
}
