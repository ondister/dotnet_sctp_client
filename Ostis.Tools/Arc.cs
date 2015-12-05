using Ostis.Sctp.Arguments;
using Ostis.Sctp.Commands;
using Ostis.Sctp.Responses;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Элемент - дуга.
    /// </summary>
    public class Arc : ElementBase
    {
        #region Свойства

        private readonly ElementBase beginElement;
        private readonly ElementBase endElement;

        /// <summary>
        /// Начальный элемент.
        /// </summary>
        public ElementBase BeginElement
        { get { return beginElement; } }

        /// <summary>
        /// Конечный элемент.
        /// </summary>
        public ElementBase EndElement
        { get { return endElement; } }

        #endregion

        private Arc(ElementType type, ElementBase beginElement, ElementBase endElement)
            : base(type)
        {
            this.beginElement = beginElement;
            this.endElement = endElement;
        }

        #region CRUD-методы

        internal static Arc Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            Arc arc = null;
            if (knowledgeBase.Commands.IsElementExist(scAddress))
            {
                ElementType type = knowledgeBase.Commands.GetElementType(scAddress);
                if (type.HasAnyType(ElementType.ArcMask_c))
                {
#warning здесь возможна бесконечная цикла если элементы дуги тоже дуги бесконечно
                    var command = new GetArcElementsCommand(scAddress);
                    var response = (GetArcElementsResponse)knowledgeBase.ExecuteCommand(command);
                    var beginElement = LoadElement(knowledgeBase, response.BeginElementAddress);
                    var endElement = LoadElement(knowledgeBase, response.EndElementAddress);
                    arc = new Arc(type, beginElement, endElement)
                    {
                        Address = scAddress,
                        State = ElementState.Synchronized
                    };
                }
            }
            return arc;
        }

        internal override bool Save(KnowledgeBase knowledgeBase)
        {
#warning Непрозрачная логика метода. Можно одновременно создать, отредактировать и удалить. Confusing зело.
#warning Вынести в родительский класс, так как дублирование кода.
            bool isSaved = false;
            if (State.HasAnyState(ElementState.New))
            {
                CreateNew(knowledgeBase);
                State = State.RemoveState(ElementState.New);
            }
            /*if (State.HasAnyState(ElementState.Edited))
            {
                Modify(knowledgeBase);
                State = State.RemoveState(ElementState.Edited);
            }*/
            if (State.HasAnyState(ElementState.Deleted))
            {
                Delete(knowledgeBase);
                State = State.RemoveState(ElementState.Deleted);
            }
            State = State.AddState(ElementState.Synchronized);
            return isSaved;
        }

        private void CreateNew(KnowledgeBase knowledgeBase)
        {
#warning См. комментарий.
            //добавлять в соответствующие коллекции элементов перед использованием в виде вершин дуг
            Address = knowledgeBase.Commands.CreateArc(Type, beginElement.Address, endElement.Address);
        }

        private bool Delete(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.DeleteElement(Address);
        }

        #endregion
    }
}
