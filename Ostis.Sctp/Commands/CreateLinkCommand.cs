namespace Ostis.Sctp.Commands
{
    internal class CreateLinkCommand : Command
    {
        public CreateLinkCommand()
            : base(0x05, 0)
        {
            Header.ArgumentsSize = 0;
#warning Значение размера аргументов должно устанавливаться в абстрактном виртуальном методе.
        }
    }
}
