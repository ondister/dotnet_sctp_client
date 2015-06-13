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
            : base(CommandCode.FindElement)
        {
            Arguments.Add(identifier);
        }
    }
}
