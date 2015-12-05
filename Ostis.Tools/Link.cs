using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Элемент - ссылка.
    /// </summary>
    public class Link : ElementBase
    {
        #region Свойства

        private LinkContent linkContent;

        /// <summary>
        /// Содержимое ссылки.
        /// </summary>
        public LinkContent LinkContent
        {
            get { return linkContent; }
            set
            {
                linkContent = value;
                OnChanged();
            }
        }

        #endregion

        private Link(LinkContent linkContent)
            : base(ElementType.Link_a)
        {
            this.linkContent = linkContent;
        }

        #region CRUD-методы

        internal static Link Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            var link = new Link(LinkContent.Invalid);
            if (knowledgeBase.Commands.IsElementExist(scAddress))
            {
                ElementType type = knowledgeBase.Commands.GetElementType(scAddress);
                if (type.IsType(ElementType.Link_a))
                {
                    link.Address = scAddress;
                    link.LinkContent = knowledgeBase.Commands.GetLinkContent(scAddress);
                    link.Type = ElementType.Link_a;
                    link.State = ElementState.Synchronized;
                }
            }
            return link;
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
            if (State.HasAnyState(ElementState.Edited))
            {
                Modify(knowledgeBase);
                State = State.RemoveState(ElementState.Edited);
            }
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
            Address = knowledgeBase.Commands.CreateLink();
            knowledgeBase.Commands.SetLinkContent(Address, linkContent);
        }

        private bool Modify(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.SetLinkContent(Address, linkContent);
        }

        private bool Delete(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.DeleteElement(Address);
        }

        #endregion

        /// <summary>
        /// Обработка собственного изменения.
        /// </summary>
        protected override void OnChanged()
        {
            State = State.RemoveState(ElementState.Synchronized);
            State = State.AddState(ElementState.Edited);
        }
    }
}
