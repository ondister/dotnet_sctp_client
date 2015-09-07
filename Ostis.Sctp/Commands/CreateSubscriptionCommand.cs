using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание подписки на события.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="CreateSubscriptionCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="SubScriptions" lang="C#" />
    /// </example>
    public class CreateSubscriptionCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Тип события.
        /// </summary>
        public EventType Type
        { get { return type; } }

        /// <summary>
        /// SC-адрес.
        /// </summary>
        public ScAddress Address
        { get { return address; } }

        private readonly EventType type;
        private readonly ScAddress address;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="type">тип события</param>
        /// <param name="address">SC-адрес</param>
		public CreateSubscriptionCommand(EventType type, ScAddress address)
            : base(CommandCode.CreateSubscription)
        {
            Arguments.Add (new EventTypeArgument(this.type = type));
			Arguments.Add(this.address = address);
        }
    }
}
