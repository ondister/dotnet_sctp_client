using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Удаление подписки на события.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="DeleteSubscriptionCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="SubScriptions" lang="C#" />
    /// </example>
    public class DeleteSubscriptionCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Событие.
        /// </summary>
        public SubscriptionId SubscriptionId
        { get { return subscriptionId; } }

        private readonly SubscriptionId subscriptionId;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="id">событие</param>
		public DeleteSubscriptionCommand(SubscriptionId id)
			: base(CommandCode.DeleteSubscription)
        {
            Arguments.Add(subscriptionId = id);
        }
    }
}
