using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс инкапсулирует коллекцию ссылок базы знаний
    /// </summary>
    /// <remarks>
    /// Класс не содержит никаких кешируемых данных о ссылках, все данные запрашиваются динамически
    /// </remarks>
    public class Links
    {
        private KnowledgeBase knowledgeBase;
        internal Links(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase = knowledgeBase;
        }
       
        
       /// <summary>
       ///  Возвращает ссылку по его адресу
       /// </summary>
       /// <param name="scAddress">Адрес ссылки</param>
       /// <returns>Найденная ссылка</returns>
        public Link this[ScAddress scAddress]
        {
            get
            {
                return new Link(knowledgeBase, scAddress);
            }
        }

        /// <summary>
        /// Добавляет ссылку с указанным контентом
        /// </summary>
        /// <param name="content">Контент для ссылки</param>
        /// <returns>Возвращает адрес созданной ссылки</returns>
        public ScAddress Add(LinkContent content)
        {
            ScAddress linkAddress = knowledgeBase.Commands.CreateLink();
            if (linkAddress != ScAddress.Invalid)
            {
                knowledgeBase.Commands.SetLinkContent(linkAddress, content);
            }
            return linkAddress;
        }
    }
}
