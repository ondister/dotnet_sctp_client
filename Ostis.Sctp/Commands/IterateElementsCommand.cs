using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
    /// </summary>
    public class IterateElementsCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="template">шаблон для поиска</param>
        public IterateElementsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateElements, 0)
        {
            Arguments.Add(template);
        }
    }
}
