using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение содержимого SC-ссылки.
    /// </summary>
    public class GetLinkContentCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес ссылки для получения содержимого.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес ссылки для получения содержимого</param>
        public GetLinkContentCommand(ScAddress address)
            : base(CommandCode.GetLinkContent)
        {
            Arguments.Add(this.address = address);
        }
    }
}
