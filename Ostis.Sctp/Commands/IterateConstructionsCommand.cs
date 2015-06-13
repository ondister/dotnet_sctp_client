using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
#warning На эту команду нет класса ответа!
    /// <summary>
    /// Команда: Итерирование конструкций.
    /// </summary>
    public class IterateConstructionsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Шаблон поиска.
        /// </summary>
        public ConstructionTemplate Template
        { get { return template; } }

        private readonly ConstructionTemplate template;

        #endregion
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="template">шаблон поиска</param>
		public IterateConstructionsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateConstructions)
        {
            Arguments.Add(this.template = template);
        }
    }
}
