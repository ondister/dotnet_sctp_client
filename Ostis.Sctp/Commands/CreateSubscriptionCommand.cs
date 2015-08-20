using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание подписки на события.
    /// </summary>
    public class CreateSubscriptionCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Тип события.
        /// </summary>
        public EventsType Type
        { get { return type; } }

        /// <summary>
        /// SC-адрес.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly EventsType type;
        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="type">тип события</param>
        /// <param name="address">SC-адрес</param>
		public CreateSubscriptionCommand(EventsType type, ScAddress address)
            : base(CommandCode.CreateSubscription)
        {
            Arguments.Add (new EventsTypeArgument(this.type = type));
			Arguments.Add(this.address = address);
        }
    }
}
