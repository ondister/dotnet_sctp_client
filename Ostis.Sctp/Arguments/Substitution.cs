using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Класс подстановка для цепочки итераторов
    /// Он определяет индекс какого элеманта первого итератора будет подставлен вместо неизвестного адреса слудующего итератора
    /// </summary>
    public class Substitution
    {

        private readonly byte firstIteratorElementIndex;

        /// <summary>
        /// Получает индекс элемента первого итератора
        /// </summary>
        /// <value>
        /// Индекс элемента первого итератора
        /// </value>
       internal byte FirstIteratorElementIndex
        {
            get { return firstIteratorElementIndex; }
        }

        private readonly byte nextIteratorElementIndex;

        /// <summary>
        /// Получает индекс элемента следующего итератора
        /// </summary>
        /// <value>
        /// Индекс элемента следующего итератора
        /// </value>
        internal byte NextIteratorElementIndex
        {
            get { return nextIteratorElementIndex; }
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Substitution"/>
        /// </summary>
        /// <param name="firstIteratorElement">Индекс элемента первого итератора</param>
        /// <param name="nextIteratorElement">Индекс элемента следующего итератора</param>
        public Substitution(byte firstIteratorElement, byte nextIteratorElement)
        {
            this.firstIteratorElementIndex = firstIteratorElement;
            this.nextIteratorElementIndex = nextIteratorElement;
        }



       
    }
}
