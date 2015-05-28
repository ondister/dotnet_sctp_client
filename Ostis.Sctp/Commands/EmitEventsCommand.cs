namespace Ostis.Sctp.Commands
{
    internal class EmitEventsCommand : Command
    {
        public EmitEventsCommand()
            : base(0x10, 0)
        {
            Header.ArgumentsSize = 0;
        }
    }
}
