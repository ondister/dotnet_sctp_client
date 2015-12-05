using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Элемент - узел.
    /// </summary>
    public class Node : ElementBase
    {
        #region Свойства

        private Identifier systemIdentifier;
        private readonly string prefix;

        /// <summary>
        /// Системный идентификатор.
        /// </summary>
        public Identifier SystemIdentifier
        {
            get { return systemIdentifier; }
            set
            {
                systemIdentifier = value;
                OnChanged();
            }
        }

        #endregion

        #region Конструкторы
        
        private Node(ElementType type, Identifier systemIdentifier)
            : base(type)
        {
            this.systemIdentifier = systemIdentifier;
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="type">тип</param>
        /// <param name="prefix">префикс</param>
        public Node(ElementType type, string prefix)
            : this(type, Identifier.Unique)
        {
            this.prefix = prefix;
            State = State.AddState(ElementState.Edited);
        }

        #endregion

        #region CRUD-методы

        internal static Node Load(KnowledgeBase knowledgeBase, ScAddress scAddress)
        {
            var node = new Node(ElementType.Unknown, Identifier.Invalid);
            if (knowledgeBase.Commands.IsElementExist(scAddress))
            {
                node.Address = scAddress;
                node.SystemIdentifier = knowledgeBase.Commands.GetNodeSysIdentifier(scAddress);
                node.Type = knowledgeBase.Commands.GetElementType(scAddress);
                node.State = ElementState.Synchronized;
            }
            return node;
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
            Address = knowledgeBase.Commands.CreateNode(Type);
        }

        private bool Modify(KnowledgeBase knowledgeBase)
        {
            if (systemIdentifier == Identifier.Unique)
            {
                SystemIdentifier = knowledgeBase.Commands.GenerateUniqueSysIdentifier(Address, prefix);
            }
            return knowledgeBase.Commands.SetSysIdentifier(Address, SystemIdentifier);
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
