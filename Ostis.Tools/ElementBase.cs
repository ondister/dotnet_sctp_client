using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostis.Sctp.Arguments;
namespace Ostis.Sctp.Tools
{
    public abstract class ElementBase
    {
        public delegate void ElementHandler(ElementBase sender);
        public event ElementHandler OnPropertyChanged;


        protected void PropertyChanged() { if (OnPropertyChanged != null) { OnPropertyChanged(this); } }
        public ScAddress Address
        { get; protected set; }

        public ElementType Type
        { get;protected set; }

        public ElementState State
        { get; internal set; }

        protected ElementBase(ElementType type)
        {
            this.Address = ScAddress.Invalid;
            this.State = ElementState.New;
            this.Type = type;
        }

        internal abstract bool Save(KnowledgeBase knowledgeBase);

        #region костыли и велосипеды
        internal static Type GetElementType(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
             ElementType type=knowledgeBase.Commands.GetElementType(scAddress);
             if (type.HasAnyType(ElementType.ArcMask_c)) { return typeof(Arc); }
            if(type.IsType(ElementType.Link_a)){return typeof(Link);}
            else { return typeof(Node); }
        }

        internal static ElementBase LoadElement(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            ElementBase loadedElement = null;
            if (ElementBase.GetElementType(knowledgeBase, scAddress) == typeof(Node)) { loadedElement = Node.Load(knowledgeBase, scAddress); }
            if (ElementBase.GetElementType(knowledgeBase, scAddress) == typeof(Link)) { loadedElement = Link.Load(knowledgeBase, scAddress); }
            if (ElementBase.GetElementType(knowledgeBase, scAddress) == typeof(Arc)) { loadedElement = Arc.Load(knowledgeBase, scAddress); }
            return loadedElement;
        }
        #endregion
    }
}
