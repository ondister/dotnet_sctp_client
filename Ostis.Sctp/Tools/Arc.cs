using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp;
using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс инкапсулирует дугу базы знаний
    /// </summary>
    public class Arc : IElement
    {
        #region реализация интерфейса IElement

        private readonly IElement beginElement;

        /// <summary>
        /// Возвращает начальный элемент дуги
        /// </summary>
        public IElement BeginElement
        {
            get { return beginElement; }
        }


        private readonly IElement endElement;

        /// <summary>
        /// Возвращает конечный элемент дуги
        /// </summary>
        public IElement EndElement
        {
            get { return endElement; }
        } 


      /// <summary>
      /// Возвращает тип дуги
      /// </summary>
        public ElementType Type
        {
            get { return knowledgeBase.Commands.GetElementType(address); }
        }

        private ScAddress address;

        /// <summary>
        /// Возвращает адрес дуги
        /// </summary>
        public ScAddress ScAddress
        {
            get { return address; }
        }

      

#endregion
      
      private KnowledgeBase knowledgeBase;

    
      internal Arc(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.address = scAddress;

          var command = new GetArcElementsCommand(scAddress);
          var response = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(command);

          beginElement = knowledgeBase.Commands.GetElementType(response.BeginElementAddress) == ElementType.Link_a ? knowledgeBase.Links[response.BeginElementAddress] : knowledgeBase.Links[response.BeginElementAddress];
          endElement = knowledgeBase.Commands.GetElementType(response.EndElementAddress) == ElementType.Link_a ? knowledgeBase.Links[response.EndElementAddress] : knowledgeBase.Links[response.EndElementAddress];         
          
      }

      private Arc(KnowledgeBase knowledgeBase)
      {
          this.knowledgeBase = knowledgeBase;
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
          IElement element = obj as IElement;
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
        /// Определяет оператор сравнения дуг
        /// </summary>
        /// <param name="element1">Дуга 1</param>
        /// <param name="element2">Дуга 2</param>
        /// <returns>Возвращает True, если адреса дуг равны</returns>
      public static bool operator ==(Arc element1, Arc element2)
      {
          bool isEqual = false;
          if (((object)element1 != null) && ((object)element2 != null))
          {
              isEqual = element1.Equals(element2);
          }

          return isEqual;
      }

      /// <summary>
      /// Определяет оператор сравнения дуг
      /// </summary>
      /// <param name="element1">Дуга 1</param>
      /// <param name="element2">Дуга 2</param>
      /// <returns>Возвращает True, если адреса дуг не равны</returns>
      public static bool operator !=(Arc element1, Arc element2)
      {
          return !(element1 == element2);
      }

    }
}
