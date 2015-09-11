using Ostis.Sctp.Arguments;
using Ostis.Sctp.Tools;
namespace Ostis.Sctp.Commands
{

    /// <summary>
    /// Команда: Итерирование конструкций.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="GetArcElementsCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="IterateConstructions" lang="C#" />
    /// </example>
    /// <remarks>
    /// Итерирование конструкций необходимо для ускорения работы сервера с итераторами и позволяет строить сложные итераторы для поиска конструкций.
    /// </remarks>
    public class IterateConstructionsCommand : Command
    {
        #region Параметры команды

        private readonly IteratorsChain iteratorsChain;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="iteratorsChain">цепочка итераторов</param>
        public IterateConstructionsCommand(IteratorsChain iteratorsChain)
            : base(CommandCode.IterateConstructions)
        {
             Arguments.Add(this.iteratorsChain=iteratorsChain);
        }
    }
}
