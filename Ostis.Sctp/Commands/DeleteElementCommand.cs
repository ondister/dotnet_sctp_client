using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Удаление SC-элемента с указанным SC-адресом.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="DeleteElementCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="DeleteNode" lang="C#" />
    /// </example>
    public class DeleteElementCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес удаляемого sc-элемента.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="address">SC-адрес удаляемого sc-элемента</param>
        public DeleteElementCommand(ScAddress address)
            : base(CommandCode.DeleteElement)
        {
            Arguments.Add(this.address = address);
        }
    }
}
