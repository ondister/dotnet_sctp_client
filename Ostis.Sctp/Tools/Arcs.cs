using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс инкапсулирует дуги базы знаний
    /// </summary>
    public class Arcs
    {
        private KnowledgeBase knowledgeBase;
        internal Arcs(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }
       
        
       /// <summary>
       /// Возвращает дугу по ее адресу
       /// </summary>
       /// <param name="scAddress">Адрес дуги</param>
       /// <returns>Найденная дуга</returns>
        public Arc this[ScAddress scAddress]
        {
            get
            {
                return new Arc(knowledgeBase, scAddress);
            }
        }

        /// <summary>
        /// Добавляет дугу в базу знаний
        /// </summary>
        /// <param name="arcType">Тип дуги</param>
        /// <param name="beginElement">Начальный элемент дуги</param>
        /// <param name="endElement">Конечный элемент дуги</param>
        /// <returns>Возвращает адрес созданной дуги</returns>
        public ScAddress Add(ElementType arcType, IElement beginElement, IElement endElement)
        {
            return knowledgeBase.Commands.CreateArc(arcType, beginElement.ScAddress, endElement.ScAddress);
        }

    }
}
