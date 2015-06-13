using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение типа SC-элемента по SC-адресу.
    /// </summary>
    public class GetElementTypeCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес элемента для получения типа.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес элемента для получения типа</param>
        public GetElementTypeCommand(ScAddress address)
            : base(CommandCode.GetElementType)
        {
            Arguments.Add(this.address = address);
        }
    }
}
