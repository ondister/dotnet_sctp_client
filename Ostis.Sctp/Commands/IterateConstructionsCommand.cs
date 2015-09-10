using Ostis.Sctp.Arguments;
using Ostis.Sctp.Tools;
namespace Ostis.Sctp.Commands
{

    /// <summary>
    /// Команда: Итерирование конструкций.
    /// </summary>
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
