using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Удаление SC-элемента с указанным SC-адресом.
    /// </summary>
    public class DeleteElementCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес удаляемого sc-элемента</param>
        public DeleteElementCommand(ScAddress address)
            : base(CommandCode.DeleteElement, 0)
        {
            Arguments.Add(address);
        }
    }
}
