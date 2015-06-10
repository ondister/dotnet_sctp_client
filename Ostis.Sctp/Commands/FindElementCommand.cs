using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск SC-элемента по его системному идентификатору.
    /// </summary>
    public class FindElementCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="identifier">идентификатор</param>
        public FindElementCommand(Identifier identifier)
            : base(CommandCode.FindElement, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<uint>((uint) identifier.BytesStream.Length));
            Arguments.Add(new Argument<Identifier>(identifier));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
