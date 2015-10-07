using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс инкапсулирует ссылку базы знаний
    /// </summary>
  public class Link:IElement
    {
      

      /// <summary>
      /// Возвращает контент ссылки
      /// </summary>
        public LinkContent Content
        {
            get { return knowledgeBase.Commands.GetLinkContent(scAddress); }
        }

        private ScAddress scAddress;

      /// <summary>
      /// Возвращает адрес ссылки
      /// </summary>
        public ScAddress ScAddress
        {
            get { return scAddress; }
        }

      
      private KnowledgeBase knowledgeBase;

    
      internal Link(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.scAddress = scAddress;
      }

      private Link(KnowledgeBase knowledgeBase)
      {
          this.knowledgeBase = knowledgeBase;
      }



      /// <summary>
      /// Возвращает тип ссылки
      /// </summary>
      public ElementType Type
      {
          get { return ElementType.Link_a; }
      }

      /// <summary>
      /// Определяет равен ли заданный объект <see cref="IElement"/> текущему объекту
      /// </summary>
      /// <param name="obj">объект <see cref="IElement"/></param>
      public bool Equals(IElement obj)
      {
          if (obj == null)
              return false;

          return obj.ScAddress.Equals(this.ScAddress);
      }

      /// <summary>
      /// Определяет равен ли заданный объект <see cref="T:System.Object"/> текущему объекту
      /// </summary>
      /// <param name="obj">объект <see cref="T:System.Object"/></param>
      public override bool Equals(object obj)
      {
          if (obj == null)
              return false;
          IElement element= obj as IElement;
          if (element as IElement == null)
              return false;
          return element.ScAddress.Equals(this.ScAddress);
      }

      /// <summary>
      /// Возвращает Hash код текущего объекта
      /// </summary>
      public override int GetHashCode()
      {
          return Convert.ToInt32(this.ScAddress.GetHashCode());
      }

      /// <summary>
      /// Определяет оператор сравнения для ссылки
      /// </summary>
      /// <param name="element1">Ссылка 1</param>
      /// <param name="element2">Ссылка 2</param>
      /// <returns>Возвращает True, если адреса ссылок равны</returns>
      public static bool operator ==(Link element1, Link element2)
      {
          bool isEqual = false;
          if (((object)element1 != null) && ((object)element2 != null))
          {
              isEqual = element1.Equals(element2);
          }

          return isEqual;
      }

      /// <summary>
      /// Определяет оператор сравнения для ссылки
      /// </summary>
      /// <param name="element1">Ссылка 1</param>
      /// <param name="element2">Ссылка 2</param>
      /// <returns>Возвращает True, если адреса ссылок не равны</returns>
      public static bool operator !=(Link element1, Link element2)
      {
          return !(element1 == element2);
      }
    }
}
