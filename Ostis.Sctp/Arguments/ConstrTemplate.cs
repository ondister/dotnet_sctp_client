using System.Collections.Generic;
using System.IO;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Шаблон поиска конструкции для команды CmdGetIterateElements
    /// </summary>
    public struct ConstructionTemplate : IArgument
    {
        private readonly List<IArgument> arguments;
        private byte[] bytes;
        private uint length;
        private readonly ConstructionTemplateType templateType;

        internal ConstructionTemplateType Type
        { get { return templateType; } }

        /// <summary>
        /// Возвращает длину массива байт шаблона
        /// </summary>
        public uint Length
        { get { return length; } }

        /// <summary>
        /// Возвращает массив байт шаблона
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

#warning Конструкторы не вызывают друг друга!
        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска исходящих дуг из указанного элемента
        /// </summary>
        /// <param name="f">Адрес начального sc-элемента</param>
        /// <param name="a1">Тип исходящей дуги дуги</param>
        /// <param name="a2">Тип конечного sc-элемента (дуги или узла)</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3F_A_A;
            arguments.Add(new Argument<ScAddress>(f));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска входящих дуг к указанному элементу.
        /// </summary>
        /// <param name="a1">Тип начального sc-элемента</param>
        /// <param name="a2">Тип входящей дуги</param>
        /// <param name="f">Адрес конечного sc-элемента</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3A_A_F;
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ScAddress>(f));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска дуги между двумя элементами.
        /// </summary>
        /// <param name="f1">Адрес начального sc-элемента</param>
        /// <param name="a1">Тип искомой дуги</param>
        /// <param name="f2">Адрес конечного sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3F_A_F;
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ScAddress>(f2));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="a1">Тип первого sc-элемента конструкции</param>
        /// <param name="a2">Тип второго sc-элемента конструкции</param>
        /// <param name="f">Адрес третьего sc-элемента</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">Тип пятого sc-элемента конструкции</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f, ElementType a3, ElementType a4)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5A_A_F_A_A;
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ScAddress>(f));
            arguments.Add(new Argument<ElementType>(a3));
            arguments.Add(new Argument<ElementType>(a4));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="a1">Тип первого sc-элемента конструкции</param>
        /// <param name="a2">Тип второго sc-элемента конструкции</param>
        /// <param name="f1">Адрес третьего sc-элемента</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">Адрес пятого sc-элемента</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f1, ElementType a3, ScAddress f2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5A_A_F_A_F;
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a3));
            arguments.Add(new Argument<ScAddress>(f2));

            CreateByteStream();
        }

        /// <summary>
        ///  Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="a2">Тип третьего sc-элемента конструкции</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">Тип пятого sc-элемента конструкции</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2, ElementType a3, ElementType a4)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5F_A_A_A_A;
            arguments.Add(new Argument<ScAddress>(f));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ElementType>(a3));
            arguments.Add(new Argument<ElementType>(a4));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f1">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="a2">Тип третьего sc-элемента конструкции</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">Адрес пятого sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ElementType a2, ElementType a3, ScAddress f2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5F_A_A_A_F;
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ElementType>(a3));
            arguments.Add(new Argument<ScAddress>(f2));

            CreateByteStream();
        }

        /// <summary>
        ///Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f1">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="f2">Адрес третьего sc-элемента</param>
        /// <param name="a2">Тип четвертого sc-элемента конструкции</param>
        /// <param name="a3">Тип пятого sc-элемента конструкции
        /// </param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ElementType a3)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5F_A_F_A_A;
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ScAddress>(f2));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ElementType>(a3));

            CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f1">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="f2">Адрес третьего sc-элемента</param>
        /// <param name="a2">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f3">Адрес пятого sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ScAddress f3)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_5F_A_F_A_F;
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ScAddress>(f2));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ScAddress>(f3));

            CreateByteStream();
        }

        private void CreateByteStream()
        {
            MemoryStream mstream = new MemoryStream();
            mstream.Write(new[] { (byte)templateType }, 0, 1);
            foreach (var argument in arguments)
            {
                mstream.Write(argument.BytesStream, 0, (int)argument.Length);
            }
            bytes = mstream.ToArray();
            length = (uint)bytes.Length;
        }
    }
}
