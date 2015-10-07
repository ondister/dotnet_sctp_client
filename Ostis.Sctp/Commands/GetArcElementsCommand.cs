using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение начального  и конечного элемента SC-дуги.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="GetArcElementsCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="GetArcElements" lang="C#" />
    /// </example>
    public class GetArcElementsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// SC-адрес дуги у которой необходимо получить начальный элемент.
        /// </summary>
        public ScAddress ArcAddress
        { get { return arcAddress; } }

        private readonly ScAddress arcAddress;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="arcAddress">SC-адрес дуги у которой необходимо получить начальный и конечный элемент</param>
        public GetArcElementsCommand(ScAddress arcAddress)
            : base(CommandCode.GetArc)
        {
            Arguments.Add(this.arcAddress = arcAddress);
        }
    }
}
