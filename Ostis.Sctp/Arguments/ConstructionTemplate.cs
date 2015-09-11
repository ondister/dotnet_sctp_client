using System.Collections.Generic;
using System.IO;


namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Шаблон поиска конструкции для команды <see cref="T:Ostis.Sctp.Commands.IterateElementsCommand"/>
    /// </summary>
    /// <remarks>
    /// <para><b>Стоит запомнить, что a(assign) обозначает неизвестный элемент, для которого известен только тип  <see cref="ElementType"/>, а f(fixed)  <see cref="ScAddress"/> изввестного элемента </b></para>
    /// </remarks>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="ConstructionTemplate"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="ConstructionTemplate" lang="C#" />
    /// </example>

   
   
    public class ConstructionTemplate : IArgument
    {
        private readonly int fixedCount;

        internal int FixedCount
        {
            get { return fixedCount; }
        } 


        private readonly List<IArgument> elements;

        internal List<IArgument> Elements
        {
            get { return elements; }
        } 

        private readonly ConstructionTemplateType templateType;

        internal ConstructionTemplateType Type
        { get { return templateType; } }

        #region Конструкторы

        private ConstructionTemplate(ConstructionTemplateType type)
        {
            elements = new List<IArgument>();
            templateType = type;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_a
        /// </summary>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_3_faa.png" />
        /// </remarks>
        /// <param name="f">адрес начального sc-элемента</param>
        /// <param name="a1">тип исходящей дуги</param>
        /// <param name="a2">тип конечного sc-элемента (дуги или узла)</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2)
            : this(ConstructionTemplateType.Fixed_Assign_Assign)
        {
            elements.Add(f);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            fixedCount = 1;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа a_a_f
        /// </summary>
        /// <param name="a1">тип начального sc-элемента</param>
        /// <param name="a2">тип входящей дуги</param>
        /// <param name="f">адрес конечного sc-элемента</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_3_aaf.png" />
        /// </remarks>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f)
            : this(ConstructionTemplateType.Assign_Assign_Fixed)
        {
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(f);
            fixedCount = 1;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_f
        /// </summary>
        /// <param name="f1">адрес начального sc-элемента</param>
        /// <param name="a1">тип искомой дуги</param>
        /// <param name="f2">адрес конечного sc-элемента</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_3_faf.png" />
        /// </remarks>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2)
            : this(ConstructionTemplateType.Fixed_Assign_Fixed)
        {
            elements.Add(f1);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(f2);
            fixedCount = 2;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа a_a_f_a_a
        /// </summary>
        /// <param name="a1">тип первого sc-элемента конструкции</param>
        /// <param name="a2">тип второго sc-элемента конструкции</param>
        /// <param name="f">адрес третьего sc-элемента</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">тип пятого sc-элемента конструкции</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_aafaa.png" />
        /// </remarks>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f, ElementType a3, ElementType a4)
            : this(ConstructionTemplateType.Assign_Assign_Fixed_Assign_Assign)
        {
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(f);
            elements.Add(new ElementTypeArgument(a3));
            elements.Add(new ElementTypeArgument(a4));
            fixedCount = 1;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа a_a_f_a_f
        /// </summary>
        /// <param name="a1">тип первого sc-элемента конструкции</param>
        /// <param name="a2">тип второго sc-элемента конструкции</param>
        /// <param name="f1">адрес третьего sc-элемента</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">адрес пятого sc-элемента</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_aafaf.png" />
        /// </remarks>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f1, ElementType a3, ScAddress f2)
            : this(ConstructionTemplateType.Assign_Assign_Fixed_Assign_Fixed)
        {
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(f1);
            elements.Add(new ElementTypeArgument(a3));
            elements.Add(f2);
            fixedCount = 2;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_a_a_a
        /// </summary>
        /// <param name="f">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="a2">тип третьего sc-элемента конструкции</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">тип пятого sc-элемента конструкции</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_faaaa.png" />
        /// </remarks>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2, ElementType a3, ElementType a4)
            : this(ConstructionTemplateType.Fixed_Assign_Assign_Assign_Assign)
        {
            elements.Add(f);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(new ElementTypeArgument(a3));
            elements.Add(new ElementTypeArgument(a4));
            fixedCount = 1;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_a_a_f
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="a2">тип третьего sc-элемента конструкции</param>
        /// <param name="a3">тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">адрес пятого sc-элемента</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_faaaf.png" />
        /// </remarks>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ElementType a2, ElementType a3, ScAddress f2)
            : this(ConstructionTemplateType.Fixed_Assign_Assign_Assign_Fixed)
        {
            elements.Add(f1);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(new ElementTypeArgument(a3));
            elements.Add(f2);
            fixedCount = 2;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_f_a_a
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="f2">адрес третьего sc-элемента</param>
        /// <param name="a2">тип четвертого sc-элемента конструкции</param>
        /// <param name="a3">тип пятого sc-элемента конструкции</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_fafaa.png" />
        /// </remarks>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ElementType a3)
            : this(ConstructionTemplateType.Fixed_Assign_Fixed_Assign_Assign)
        {
            elements.Add(f1);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(f2);
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(new ElementTypeArgument(a3));
            fixedCount = 2;
        }

        /// <summary>
        /// Инициализирует новый шаблон конструкции типа f_a_f_a_f
        /// </summary>
        /// <param name="f1">адрес первого sc-элемента</param>
        /// <param name="a1">тип второго sc-элемента конструкции</param>
        /// <param name="f2">адрес третьего sc-элемента</param>
        /// <param name="a2">тип четвертого sc-элемента конструкции</param>
        /// <param name="f3">адрес пятого sc-элемента</param>
        /// <remarks>
        /// <para>На рисунке показаны порядок перечисления и обозначения элементов шаблона</para>
        /// <para><b>scg код шаблона выглядит так:</b></para>
        /// <img src="media/template_5_fafaf.png" />
        /// </remarks>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ScAddress f3)
            : this(ConstructionTemplateType.Fixed_Assign_Fixed_Assign_Fixed)
        {
            elements.Add(f1);
            elements.Add(new ElementTypeArgument(a1));
            elements.Add(f2);
            elements.Add(new ElementTypeArgument(a2));
            elements.Add(f3);
            fixedCount = 3;
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
                foreach (var argument in elements)
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
