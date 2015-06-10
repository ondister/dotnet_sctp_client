using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
#warning На эту команду нет класса ответа!
    /// <summary>
    /// Команда: Итерирование конструкций.
    /// </summary>
    public class IterateConstructionsCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="template">шаблон поиска</param>
		public IterateConstructionsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateConstructions, 0)
        {
            uint argumentsSize = 0;
            Arguments.Add(new Argument<ConstructionTemplate>(template));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
