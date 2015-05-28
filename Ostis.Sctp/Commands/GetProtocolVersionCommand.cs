namespace Ostis.Sctp.Commands
{
    internal class GetProtocolVersionCommand : Command
    {
		public GetProtocolVersionCommand()
            : base(0xa3, 0)
        {
            Header.ArgumentsSize = 0;
        }
    }
}
