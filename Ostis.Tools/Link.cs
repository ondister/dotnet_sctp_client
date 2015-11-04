using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    public class Link:ElementBase
    {
        private LinkContent linkContent;

        public LinkContent LinkContent
        {
            get { return linkContent; }
            set 
            {
                linkContent = value;
                base.PropertyChanged();
            }
        }

       static void link_OnPropertyChanged(ElementBase sender)
        {
            sender.State = sender.State.RemoveState(ElementState.Synchronized);
            sender.State = sender.State.AddState(ElementState.Edited);
        }
         public Link(LinkContent linkContent)
            : base(ElementType.Link_a)
        {
            this.linkContent = linkContent;
         }
        internal static Link Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            Link link = new Link(LinkContent.Invalid);
            bool isExist = knowledgeBase.Commands.IsElementExist(scAddress);
            if (isExist == true)
            {
                ElementType type = knowledgeBase.Commands.GetElementType(scAddress);
                 bool isLink= type.IsType(ElementType.Link_a);
                 if (isLink == true)
                 {
                     link.Address = scAddress;
                     link.LinkContent = knowledgeBase.Commands.GetLinkContent(scAddress);
                     link.Type = ElementType.Link_a;
                     link.State = ElementState.Synchronized;
                     link.OnPropertyChanged += link_OnPropertyChanged;
                 }
            }
            return link;
        }

        internal override bool Save(KnowledgeBase knowledgeBase)
        {

            bool isSaved = false;
            if (base.State.HasAnyState(ElementState.New))
            {
                this.CreateNew(knowledgeBase);
                base.State = base.State.RemoveState(ElementState.New);
            }
            if (base.State.HasAnyState(ElementState.Edited))
            {
                this.Modify(knowledgeBase);
                base.State = base.State.RemoveState(ElementState.Edited);
            }
            if (base.State.HasAnyState(ElementState.Deleted))
            {
                this.Delete(knowledgeBase);
                base.State = base.State.RemoveState(ElementState.Deleted);
            }
            base.State = base.State.AddState(ElementState.Synchronized);
            return isSaved;
        }

        private void CreateNew(KnowledgeBase knowledgeBase)
        {
            base.Address = knowledgeBase.Commands.CreateLink();
            knowledgeBase.Commands.SetLinkContent(base.Address, this.linkContent);

        }

        private bool Modify(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.SetLinkContent(base.Address, this.linkContent);
        }


        private bool Delete(KnowledgeBase knowledgeBase)
        {
            return knowledgeBase.Commands.DeleteElement(base.Address);
        }
    }
}
