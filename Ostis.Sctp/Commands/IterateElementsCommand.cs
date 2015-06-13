using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
    /// </summary>
    public class IterateElementsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Шаблон для поиска.
        /// </summary>
        public ConstructionTemplate Template
        { get { return template; } }

        private readonly ConstructionTemplate template;

        #endregion
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="template">шаблон для поиска</param>
        public IterateElementsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateElements)
        {
            Arguments.Add(this.template = template);
        }
    }
}
