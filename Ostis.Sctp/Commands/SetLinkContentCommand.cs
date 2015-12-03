using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка содержимого SC-ссылки.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="SetLinkContentCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="SetLinkContent" lang="C#" />
    /// </example>
    public class SetLinkContentCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес ссылки.
        /// </summary>
        public ScAddress LinkAddress
        { get { return linkAddress; } }

        /// <summary>
        /// Данные устанавливаемого содержимого.
        /// </summary>
        public LinkContent Content
        { get { return content; } }

        private readonly ScAddress linkAddress;
        private readonly LinkContent content;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="linkAddress">SC-адрес ссылки</param>
        /// <param name="content">данные устанавливаемого содержимого</param>
        public SetLinkContentCommand(ScAddress linkAddress, LinkContent content)
            : base(CommandCode.SetLinkContent)
        {
            Arguments.Add(this.linkAddress = linkAddress);
            Arguments.Add(new LinkContent(content.Data.Length));//это число байт контента
            Arguments.Add(this.content = content);
        }
    }
}
