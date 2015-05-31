using System;

namespace Ostis.Sctp.Arguments
{
#warning Переиметовать члены перечисления.
    /// <summary>
    /// Тип элемента
    /// </summary>
    [Flags]
    public enum ElementType : ushort
    {
        /// <summary>
        /// Не известный или не указанный тип
        /// </summary>
        unknown = 0x00,

        /// <summary>
        /// sc-узел общего вида
        /// </summary>
        sc_type_node = 0x1,

        /// <summary>
        /// sc-ссылка общего вида
        /// </summary>
        sc_type_link = 0x2,

        /// <summary>
        /// sc-ребро общего вида
        /// </summary>
        sc_type_edge_common = 0x4,

        /// <summary>
        /// sc-дуга общего вида
        /// </summary>
        sc_type_arc_common = 0x8,

        /// <summary>
        /// sc-дуга принадлежностии
        /// </summary>
        sc_type_arc_access = 0x10,

        // sc-element constant
        /// <summary>
        /// Константный тип sc-элемента
        /// </summary>
        sc_type_const = 0x20,

        /// <summary>
        /// Переменный тип sc-элемента
        /// </summary>
        sc_type_var = 0x40,

        //sc-element positivity
        /// <summary>
        /// Позитивная sc-дуга
        /// </summary>
        sc_type_arc_pos = 0x80,

        /// <summary>
        /// Негативная sc-дуга
        /// </summary>
        sc_type_arc_neg = 0x100,

        /// <summary>
        /// Нечеткая sc-дуга
        /// </summary>
        sc_type_arc_fuz = 0x200,

        //sc-element premanently
        /// <summary>
        /// Нестационарная sc-дуга
        /// </summary>
        sc_type_arc_temp = 0x400,

        /// <summary>
        /// Стационарная sc-дуга
        /// </summary>
        sc_type_arc_perm = 0x800,

        //struct node types
        /// <summary>
        /// sc-узел, обозначающий небинарную связку
        /// </summary>
        sc_type_node_tuple = (0x80),

        /// <summary>
        /// sc-узел, обозначающий структуру
        /// </summary>
        sc_type_node_struct = (0x100),

        /// <summary>
        /// sc-узел, обозначающий ролевое отношение
        /// </summary>
        sc_type_node_role = (0x200),

        /// <summary>
        /// sc-узел, обозначающий неролевое отношение
        /// </summary>
        sc_type_node_norole = (0x400),

        /// <summary>
        /// sc-узел, не являющейся отношением
        /// </summary>
        sc_type_node_class = (0x800),

        /// <summary>
        /// sc-узел, обозначающий абстрактный объект, не являющийся множеством
        /// </summary>
        sc_type_node_abstract = (0x1000),

        /// <summary>
        /// sc-узел, обозначающий материальный объект
        /// </summary>
        sc_type_node_material = (0x2000),

        /// <summary>
        /// Константный sc-узел
        /// </summary>
        sc_type_node_const = (sc_type_node | sc_type_const),

        /// <summary>
        /// Позитивная константная стационарная sc-дуга принадлежности
        /// </summary>
        sc_type_arc_pos_const_perm_acc = (sc_type_arc_access | sc_type_const | sc_type_arc_pos | sc_type_arc_perm),

        /// <summary>
        /// Позитивная константная стационарная sc-дуга общего вида
        /// </summary>
        sc_type_arc_const_comm = (sc_type_arc_common | sc_type_const),

        // type mask
        /// <summary>
        /// Маска, означающая все sc-элементы
        /// </summary>
        sc_type_element_mask = (sc_type_node | sc_type_link | sc_type_edge_common | sc_type_arc_common | sc_type_arc_access),

        /// <summary>
        /// Маска константности/переменности
        /// </summary>
        sc_type_constancy_mask = (sc_type_const | sc_type_var),

        /// <summary>
        /// Маска позитивности/негативности/нечеткости
        /// </summary>
        sc_type_positivity_mask = (sc_type_arc_pos | sc_type_arc_neg | sc_type_arc_fuz),

        /// <summary>
        /// Маска стационарности/нестационарности
        /// </summary>
        sc_type_permanency_mask = (sc_type_arc_perm | sc_type_arc_temp),

        /// <summary>
        /// Маска типов узлов
        /// </summary>
        sc_type_node_struct_mask = (sc_type_node_tuple | sc_type_node_struct | sc_type_node_role | sc_type_node_norole | sc_type_node_class | sc_type_node_abstract | sc_type_node_material),

        /// <summary>
        /// Маска типов sc-коннекторов
        /// </summary>
        sc_type_arc_mask = (sc_type_arc_access | sc_type_arc_common | sc_type_edge_common),

    }
}
