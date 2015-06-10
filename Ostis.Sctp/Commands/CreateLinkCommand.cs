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
        {
            Header.ArgumentsSize = 0;
#warning Значение размера аргументов должно устанавливаться в абстрактном виртуальном методе.
        }
    }
}
