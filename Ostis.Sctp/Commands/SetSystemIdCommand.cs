using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка системного идентификатора SC-элемента.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="SetSystemIdCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="SetSystemID" lang="C#" />
    /// </example>
    public class SetSystemIdCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC Адрес узла для которого создается идентификатор.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        /// <summary>
        /// Идентификатор для узла.
        /// </summary>
        public Identifier Identifier
        { get { return identifier; } }

        private readonly ScAddress address;
        private readonly Identifier identifier;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="address">адрес SC-эелемента</param>
        /// <param name="identifier">идентификатор</param>
        public SetSystemIdCommand(ScAddress address, Identifier identifier)
            : base(CommandCode.SetSystemId)
        {
            Arguments.Add(this.address = address);
            Arguments.Add(this.identifier = identifier);
        }
    }
}
