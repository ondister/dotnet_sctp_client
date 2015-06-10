using System;

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
            : base(0x0c, 0)
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
