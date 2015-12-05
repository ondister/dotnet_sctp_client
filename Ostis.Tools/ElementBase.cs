using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Некоторый элемент базы знаний.
    /// </summary>
    public abstract class ElementBase
    {
        #region Свойства

        /// <summary>
        /// SC-адрес.
        /// </summary>
        public ScAddress Address
        { get; protected set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public ElementType Type
        { get;protected set; }

        /// <summary>
        /// Состояние синхронизации.
        /// </summary>
        public ElementState State
        { get; internal set; }

        #endregion

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="type">тип</param>
        protected ElementBase(ElementType type)
        {
            Address = ScAddress.Invalid;
            State = ElementState.New;
            Type = type;
        }

        /// <summary>
        /// Сохранение.
        /// </summary>
        /// <param name="knowledgeBase">база знаний</param>
        /// <returns><b>true</b>, если сохранено успешно, иначе - <b>false</b></returns>
        internal abstract bool Save(KnowledgeBase knowledgeBase);

        /// <summary>
        /// Обработка собственного изменения.
        /// </summary>
        protected virtual void OnChanged()
        { }

        #region костыли и велосипеды
#warning Kill it with fire!
        internal static Type GetElementType(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            ElementType type = knowledgeBase.Commands.GetElementType(scAddress);
            if (type.HasAnyType(ElementType.ArcMask_c))
            {
                return typeof (Arc);
            }
            if (type.IsType(ElementType.Link_a))
            {
                return typeof (Link);
            }
            else
            {
                return typeof (Node);
            }
        }

        internal static ElementBase LoadElement(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            ElementBase loadedElement = null;
            if (GetElementType(knowledgeBase, scAddress) == typeof (Node))
            {
                loadedElement = Node.Load(knowledgeBase, scAddress);
            }
            if (GetElementType(knowledgeBase, scAddress) == typeof (Link))
            {
                loadedElement = Link.Load(knowledgeBase, scAddress);
            }
            if (GetElementType(knowledgeBase, scAddress) == typeof (Arc))
            {
                loadedElement = Arc.Load(knowledgeBase, scAddress);
            }
            return loadedElement;
        }

        #endregion
    }
}
