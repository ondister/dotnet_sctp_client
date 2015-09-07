namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение версии протокола. <b>Команда не реализована на сервере, но заявлена в документации!!!</b>
    /// </summary>
    /// <remarks> <b>Команда не реализована на сервере, но заявлена в документации</b></remarks>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="GetProtocolVersionCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="GetProtocolVersion" lang="C#" />
    /// </example>
    public class GetProtocolVersionCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
		public GetProtocolVersionCommand()
            : base(CommandCode.GetProtocolVersion)
        { }
    }
}
