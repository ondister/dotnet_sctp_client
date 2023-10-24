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

        protected override void CreateNew(KnowledgeBase knowledgeBase)
        {
            Address = knowledgeBase.Commands.CreateLink();
            knowledgeBase.Commands.SetLinkContent(Address, linkContent);
        }

        protected override bool Modify(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.SetLinkContent(Address, linkContent);
        }

        protected override bool Delete(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.DeleteElement(Address);
        }

        #endregion
    }
}
