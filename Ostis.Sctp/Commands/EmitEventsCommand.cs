namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Запрос всех произошедших событий.
    /// </summary>
    public class EmitEventsCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public EmitEventsCommand()
            : base(CommandCode.EmitEvents, 0)
        {
            Header.ArgumentsSize = 0;
        }
    }
}
