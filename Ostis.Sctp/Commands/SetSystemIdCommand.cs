using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка системного идентификатора SC-элемента.
    /// </summary>
    public class SetSystemIdCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">адрес SC-эелемента</param>
        /// <param name="identifier">идентификатор</param>
        public SetSystemIdCommand(ScAddress address, Identifier identifier)
            : base(CommandCode.SetSystemId, 0)
        {
            Arguments.Add(new Argument<ScAddress>(address));
            Arguments.Add(new Argument<uint>((uint) identifier.BytesStream.Length));
            Arguments.Add(new Argument<Identifier>(identifier));
        }
    }
}
