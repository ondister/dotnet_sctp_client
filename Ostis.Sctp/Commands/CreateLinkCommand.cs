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
            : base(0x05, 0)
        {
            Header.ArgumentsSize = 0;
#warning Значение размера аргументов должно устанавливаться в абстрактном виртуальном методе.
        }
    }
}
