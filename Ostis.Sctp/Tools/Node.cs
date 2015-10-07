using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Класс инкапсулирует узел базы знаний
    /// </summary>
    /// <remarks>
    /// Узел нельзя создать вне базы знаний, он не содержит никаких кешируемых данных об узле, все данные запрашиваются динамически
    /// </remarks>
  public class Node:IElement
    {
      

        private Identifier sysIdentifier;

      /// <summary>
      /// Возвращает системный идентификатор узла
      /// </summary>
        public Identifier SysIdentifier
        {
            get { return knowledgeBase.Commands.GetNodeSysIdentifier(scAddress); }
        }

        private ScAddress scAddress;

      /// <summary>
      /// Возвращает адрес узла
      /// </summary>
        public ScAddress ScAddress
        {
            get { return scAddress; }
        }


      /// <summary>
      /// Возвращает тип узла
      /// </summary>
        public ElementType Type
        {
            get { return knowledgeBase.Commands.GetElementType(scAddress); }
        }

      private KnowledgeBase knowledgeBase;

      internal Node(KnowledgeBase knowledgeBase,Identifier sysIdentifier)
          :this(knowledgeBase)
      {
          this.sysIdentifier = sysIdentifier;
          this.scAddress = knowledgeBase.Commands.GetNodeAddress(sysIdentifier);
      }

      internal Node(KnowledgeBase knowledgeBase, ScAddress scAddress)
          : this(knowledgeBase)
      {
          this.scAddress = scAddress;
      }

      private Node(KnowledgeBase knowledgeBase)
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
      /// Оператор сравнения узлов
      /// </summary>
      /// <param name="element1">Узел 1</param>
      /// <param name="element2">Узел 2</param>
      /// <returns>Возвращает True, если адреса узлов равны</returns>
      public static bool operator ==(Node element1, Node element2)
      {
          bool isEqual = false;
          if (((object)element1 != null) && ((object)element2 != null))
          {
              isEqual = element1.Equals(element2);
          }

          return isEqual;
      }

      /// <summary>
      /// Оператор сравнения узлов
      /// </summary>
      /// <param name="element1">Узел 1</param>
      /// <param name="element2">Узел 2</param>
      /// <returns>Возвращает True, если адреса узлов не равны</returns>
      public static bool operator !=(Node element1, Node element2)
      {
          return !(element1 == element2);
      }

    }
}
