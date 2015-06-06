using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Тип элемента.
    /// </summary>
    [Flags]
    public enum ElementType : ushort
    {
        /// <summary>
        /// Неизвестный или не указан.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// SC-узел общего вида.
        /// </summary>
        Node = 0x1,

        /// <summary>
        /// SC-ссылка общего вида.
        /// </summary>
        Link = 0x2,

        /// <summary>
        /// SC-ребро общего вида.
        /// </summary>
        CommonEdge = 0x4,

        /// <summary>
        /// SC-дуга общего вида.
        /// </summary>
        CommonArc = 0x8,

        /// <summary>
        /// SC-дуга принадлежности.
        /// </summary>
        AccessArg = 0x10,

        /// <summary>
        /// Константный тип SC-элемента.
        /// </summary>
        Constant = 0x20,

        /// <summary>
        /// Переменный тип SC-элемента.
        /// </summary>
        Variable = 0x40,

        /// <summary>
        /// Позитивная SC-дуга.
        /// </summary>
        PositiveArc = 0x80,

        /// <summary>
        /// Негативная SC-дуга.
        /// </summary>
        NegativeArc = 0x100,

        /// <summary>
        /// Нечеткая SC-дуга.
        /// </summary>
        FuzzyArc = 0x200,

        /// <summary>
        /// Нестационарная SC-дуга.
        /// </summary>
        TemporaryArc = 0x400,

        /// <summary>
        /// Стационарная SC-дуга.
        /// </summary>
        PermanetArc = 0x800,

        /// <summary>
        /// SC-узел, обозначающий небинарную связку.
        /// </summary>
        TupleNode = 0x80,

        /// <summary>
        /// SC-узел, обозначающий структуру.
        /// </summary>
        StructureNode = 0x100,

        /// <summary>
        /// SC-узел, обозначающий ролевое отношение.
        /// </summary>
        RoleNode = 0x200,

        /// <summary>
        /// SC-узел, обозначающий неролевое отношение.
        /// </summary>
        NonRoleNode = 0x400,

        /// <summary>
        /// SC-узел, не являющейся отношением.
        /// </summary>
        ClassNode = 0x800,

        /// <summary>
        /// SC-узел, обозначающий абстрактный объект, не являющийся множеством.
        /// </summary>
        AbstractNode = 0x1000,

        /// <summary>
        /// SC-узел, обозначающий материальный объект.
        /// </summary>
        MaterialNode = 0x2000,

        /// <summary>
        /// Константный SC-узел.
        /// </summary>
        ConstantNode = Node | Constant,

        /// <summary>
        /// Позитивная константная стационарная SC-дуга принадлежности.
        /// </summary>
        PositiveConstantPermanetAccessArc = (AccessArg | Constant | PositiveArc | PermanetArc),

        /// <summary>
        /// Позитивная константная стационарная SC-дуга общего вида.
        /// </summary>
        ConstantCommonArc = (CommonArc | Constant),

        /// <summary>
        /// Маска, означающая все SC-элементы.
        /// </summary>
        AnyElementMask = (Node | Link | CommonEdge | CommonArc | AccessArg),

        /// <summary>
        /// Маска константности/переменности.
        /// </summary>
        ConstantOrVariableMask = (Constant | Variable),

        /// <summary>
        /// Маска позитивности/негативности/нечеткости.
        /// </summary>
        PositivityMask = (PositiveArc | NegativeArc | FuzzyArc),

        /// <summary>
        /// Маска стационарности/нестационарности,
        /// </summary>
        PermanencyMask = (PermanetArc | TemporaryArc),

        /// <summary>
        /// Маска типов узлов.
        /// </summary>
        NodeOrStructureMask = (TupleNode | StructureNode | RoleNode | NonRoleNode | ClassNode | AbstractNode | MaterialNode),

        /// <summary>
        /// Маска типов SC-коннекторов.
        /// </summary>
        ArcMask = (AccessArg | CommonArc | CommonEdge),
    }
}
