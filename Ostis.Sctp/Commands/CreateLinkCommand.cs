namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание новой SC-ссылки.
    /// </summary>
    public class CreateLinkCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public CreateLinkCommand()
            : base(CommandCode.CreateLink, 0)
        { }
    }
}
