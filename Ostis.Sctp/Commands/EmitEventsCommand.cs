namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Запрос всех произошедших событий.
    /// </summary>
    public class EmitEventsCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        public EmitEventsCommand()
            : base(CommandCode.EmitEvents)
        { }
    }
}
