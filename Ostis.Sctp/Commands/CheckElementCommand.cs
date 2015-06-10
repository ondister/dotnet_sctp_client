using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Проверка существования элемента с указанным SC-адресом.
    /// </summary>
    public class CheckElementCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес проверяемого SC-элемента</param>
        public CheckElementCommand(ScAddress address)
            : base(CommandCode.CheckElement, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(address));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
