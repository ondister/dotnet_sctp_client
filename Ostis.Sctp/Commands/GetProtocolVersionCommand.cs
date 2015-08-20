namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение версии протокола.
    /// </summary>
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
