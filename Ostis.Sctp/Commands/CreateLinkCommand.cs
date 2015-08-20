namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        public CreateLinkCommand()
            : base(CommandCode.CreateLink)
        { }
    }
}
