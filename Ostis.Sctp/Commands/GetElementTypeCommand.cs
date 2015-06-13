using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение типа SC-элемента по SC-адресу.
    /// </summary>
    public class GetElementTypeCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес элемента для получения типа</param>
        public GetElementTypeCommand(ScAddress address)
            : base(CommandCode.GetElementType, 0)
        {
            Arguments.Add(address);
        }
    }
}
