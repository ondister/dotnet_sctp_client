using System;

namespace Ostis.Sctp.Arguments
{
#warning Переиметовать члены перечисления.
//наименования взяты из кода Корончика на с, я понимаю, что не по фэншую, но зато всем понятно, если читать исходники на разных языках
    /// <summary>
    /// Тип элемента.
    /// </summary>
    [Flags]
    public enum ElementType : ushort
    {
        /// <summary>
        /// Неизвестный или не указан.
        /// </summary>
        unknown = 0x00,

        /// <summary>
        /// SC-узел общего вида.
        /// </summary>
        sc_type_node = 0x1,

        /// <summary>
        /// SC-ссылка общего вида.
        /// </summary>
        sc_type_link = 0x2,

        /// <summary>
        /// SC-ребро общего вида.
        /// </summary>
        sc_type_edge_common = 0x4,

        /// <summary>
        /// SC-дуга общего вида.
        /// </summary>
        sc_type_arc_common = 0x8,

        /// <summary>
        /// SC-дуга принадлежности.
        /// </summary>
        sc_type_arc_access = 0x10,

        /// <summary>
        /// Константный тип SC-элемента.
        /// </summary>
        sc_type_const = 0x20,

        /// <summary>
        /// Переменный тип SC-элемента.
        /// </summary>
        sc_type_var = 0x40,

        /// <summary>
        /// Позитивная SC-дуга.
        /// </summary>
        sc_type_arc_pos = 0x80,

        /// <summary>
        /// Негативная SC-дуга.
        /// </summary>
        sc_type_arc_neg = 0x100,

        /// <summary>
        /// Нечеткая SC-дуга.
        /// </summary>
        sc_type_arc_fuz = 0x200,

        /// <summary>
        /// Нестационарная SC-дуга.
        /// </summary>
        sc_type_arc_temp = 0x400,

        /// <summary>
        /// Стационарная SC-дуга.
        /// </summary>
        sc_type_arc_perm = 0x800,

        /// <summary>
        /// SC-узел, обозначающий небинарную связку.
        /// </summary>
        sc_type_node_tuple = (0x80),

        /// <summary>
        /// SC-узел, обозначающий структуру.
        /// </summary>
        sc_type_node_struct = (0x100),

        /// <summary>
        /// SC-узел, обозначающий ролевое отношение.
        /// </summary>
        sc_type_node_role = (0x200),

        /// <summary>
        /// SC-узел, обозначающий неролевое отношение.
        /// </summary>
        sc_type_node_norole = (0x400),

        /// <summary>
        /// SC-узел, не являющейся отношением.
        /// </summary>
        sc_type_node_class = (0x800),

        /// <summary>
        /// SC-узел, обозначающий абстрактный объект, не являющийся множеством.
        /// </summary>
        sc_type_node_abstract = (0x1000),

        /// <summary>
        /// SC-узел, обозначающий материальный объект.
        /// </summary>
        sc_type_node_material = (0x2000),

        /// <summary>
        /// Константный SC-узел.
        /// </summary>
        sc_type_node_const = (sc_type_node | sc_type_const),

        /// <summary>
        /// Позитивная константная стационарная SC-дуга принадлежности.
        /// </summary>
        sc_type_arc_pos_const_perm_acc = (sc_type_arc_access | sc_type_const | sc_type_arc_pos | sc_type_arc_perm),

        /// <summary>
        /// Позитивная константная стационарная SC-дуга общего вида.
        /// </summary>
        sc_type_arc_const_comm = (sc_type_arc_common | sc_type_const),

        /// <summary>
        /// Маска, означающая все SC-элементы.
        /// </summary>
        sc_type_element_mask = (sc_type_node | sc_type_link | sc_type_edge_common | sc_type_arc_common | sc_type_arc_access),

        /// <summary>
        /// Маска константности/переменности.
        /// </summary>
        sc_type_constancy_mask = (sc_type_const | sc_type_var),

        /// <summary>
        /// Маска позитивности/негативности/нечеткости.
        /// </summary>
        sc_type_positivity_mask = (sc_type_arc_pos | sc_type_arc_neg | sc_type_arc_fuz),

        /// <summary>
        /// Маска стационарности/нестационарности,
        /// </summary>
        sc_type_permanency_mask = (sc_type_arc_perm | sc_type_arc_temp),

        /// <summary>
        /// Маска типов узлов.
        /// </summary>
        sc_type_node_struct_mask = (sc_type_node_tuple | sc_type_node_struct | sc_type_node_role | sc_type_node_norole | sc_type_node_class | sc_type_node_abstract | sc_type_node_material),

        /// <summary>
        /// Маска типов SC-коннекторов.
        /// </summary>
        sc_type_arc_mask = (sc_type_arc_access | sc_type_arc_common | sc_type_edge_common),
    }
}
