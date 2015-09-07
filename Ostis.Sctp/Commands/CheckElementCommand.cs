using Ostis.Sctp.Arguments;


namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Проверка существования элемента с указанным SC-адресом.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="CheckElementCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="CheckElement" lang="C#" />
    /// </example>
    public class CheckElementCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес проверяемого SC-элемента.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly ScAddress address;

        #endregion

        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="address">SC-адрес проверяемого SC-элемента</param>
        public CheckElementCommand(ScAddress address)
            : base(CommandCode.CheckElement)
        {
            Arguments.Add(this.address = address);
        }
    }
}
