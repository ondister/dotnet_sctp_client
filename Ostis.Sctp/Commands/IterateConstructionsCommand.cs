using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
#warning Эта команда не реализована, ибо не понятны чаяния Корончика
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
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="template">шаблон поиска</param>
		public IterateConstructionsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateConstructions)
        {
            Arguments.Add(this.template = template);
        }
    }
}
