using System.Collections.Generic;
using System.IO;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Шаблон поиска конструкции для команды CmdGetIterateElements.
    /// </summary>
    public struct ConstructionTemplate : IArgument
    {
        private readonly List<IArgument> arguments;
        private readonly ConstructionTemplateType templateType;

        internal ConstructionTemplateType Type
        { get { return templateType; } }

        #region Конструкторы

        private ConstructionTemplate(ConstructionTemplateType type)
        {
            arguments = new List<IArgument>();
            templateType = type;
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f">адрес начального sc-элемента</param>
        /// <param name="a1">тип исходящей дуги дуги</param>
        /// <param name="a2">тип конечного sc-элемента (дуги или узла)</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2)
            : this(ConstructionTemplateType.t_3F_A_A)
        {
            arguments.Add(f);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="a1">тип начального sc-элемента</param>
        /// <param name="a2">тип входящей дуги</param>
        /// <param name="f">адрес конечного sc-элемента</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f)
            : this(ConstructionTemplateType.t_3A_A_F)
        {
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(f);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f1">адрес начального sc-элемента</param>
        /// <param name="a1">тип искомой дуги</param>
        /// <param name="f2">адрес конечного sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2)
            : this(ConstructionTemplateType.t_3F_A_F)
        {
            arguments.Add(f1);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(f2);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="a1">тип первого sc-элемента конструкции</param>
        /// <param name="a2">тип второго sc-элемента конструкции</param>
        /// <param name="f">адрес третьего sc-элемента</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">тип пятого sc-элемента конструкции</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f, ElementType a3, ElementType a4)
            : this(ConstructionTemplateType.t_5A_A_F_A_A)
        {
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(f);
            arguments.Add(new ElementTypeArgument(a3));
            arguments.Add(new ElementTypeArgument(a4));
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="a1">тип первого sc-элемента конструкции</param>
        /// <param name="a2">тип второго sc-элемента конструкции</param>
        /// <param name="f1">адрес третьего sc-элемента</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">адрес пятого sc-элемента</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f1, ElementType a3, ScAddress f2)
            : this(ConstructionTemplateType.t_5A_A_F_A_F)
        {
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(f1);
            arguments.Add(new ElementTypeArgument(a3));
            arguments.Add(f2);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="a2">тип третьего sc-элемента конструкции</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">тип пятого sc-элемента конструкции</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2, ElementType a3, ElementType a4)
            : this(ConstructionTemplateType.t_5F_A_A_A_A)
        {
            arguments.Add(f);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(new ElementTypeArgument(a3));
            arguments.Add(new ElementTypeArgument(a4));
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="a2">тип третьего sc-элемента конструкции</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">адрес пятого sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ElementType a2, ElementType a3, ScAddress f2)
            : this(ConstructionTemplateType.t_5F_A_A_A_F)
        {
            arguments.Add(f1);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(new ElementTypeArgument(a3));
            arguments.Add(f2);
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="f2">адрес третьего sc-элемента</param>
        /// <param name="a2">тип четвертого sc-элемента конструкции</param>
        /// <param name="a3">тип пятого sc-элемента конструкции</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ElementType a3)
            : this(ConstructionTemplateType.t_5F_A_F_A_A)
        {
            arguments.Add(f1);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(f2);
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(new ElementTypeArgument(a3));
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="f2">адрес третьего sc-элемента</param>
        /// <param name="a2">тип четвертого sc-элемента конструкции</param>
        /// <param name="f3">адрес пятого sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ScAddress f3)
            : this(ConstructionTemplateType.t_5F_A_F_A_F)
        {
            arguments.Add(f1);
            arguments.Add(new ElementTypeArgument(a1));
            arguments.Add(f2);
            arguments.Add(new ElementTypeArgument(a2));
            arguments.Add(f3);
        }

        #endregion

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(new[] { (byte) templateType }, 0, 1);
                foreach (var argument in arguments)
                {
                    var bytes = argument.GetBytes();
                    stream.Write(bytes, 0, bytes.Length);
                }
                return stream.ToArray();
            }
        }

        #endregion
    }
}
