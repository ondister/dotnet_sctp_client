namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение версии протокола.
    /// </summary>
    public class GetProtocolVersionCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
		public GetProtocolVersionCommand()
            : base(0xa3, 0)
        {
            Header.ArgumentsSize = 0;
        }
    }
}
