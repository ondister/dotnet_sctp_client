using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение содержимого SC-ссылки.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="GetLinkContentCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="GetLinkContent" lang="C#" />
    /// </example>
    public class GetLinkContentCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес ссылки для получения содержимого.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="address">SC-адрес ссылки для получения содержимого</param>
        public GetLinkContentCommand(ScAddress address)
            : base(CommandCode.GetLinkContent)
        {
            Arguments.Add(this.address = address);
        }
    }
}
