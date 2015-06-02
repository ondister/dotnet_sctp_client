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
        private byte[] bytes;
        private uint length;
        private readonly ConstructionTemplateType templateType;

        internal ConstructionTemplateType Type
        { get { return templateType; } }

        /// <summary>
        /// Длина массива байт шаблона.
        /// </summary>
#warning Поле и свойство явно лишние.
//пока не лишнее в некоторых случаях нужно знать длину аргумента
        public uint Length
        { get { return length; } }

        /// <summary>
        /// Массив байт шаблона.
        /// </summary>
        public byte[] BytesStream
        { get { return bytes; } }

#warning Конструкторы не вызывают друг друга!
//тут я не понял что по чем, поясни для докторов
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f">адрес начального sc-элемента</param>
        /// <param name="a1">тип исходящей дуги дуги</param>
        /// <param name="a2">тип конечного sc-элемента (дуги или узла)</param>
        public ConstructionTemplate(ScAddress f, ElementType a1, ElementType a2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3F_A_A;
            arguments.Add(new Argument<ScAddress>(f));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));

            createByteStream();
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="a1">тип начального sc-элемента</param>
        /// <param name="a2">тип входящей дуги</param>
        /// <param name="f">адрес конечного sc-элемента</param>
        public ConstructionTemplate(ElementType a1, ElementType a2, ScAddress f)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3A_A_F;
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ElementType>(a2));
            arguments.Add(new Argument<ScAddress>(f));

            createByteStream();
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="f1">адрес начального sc-элемента</param>
        /// <param name="a1">тип искомой дуги</param>
        /// <param name="f2">адрес конечного sc-элемента</param>
        public ConstructionTemplate(ScAddress f1, ElementType a1, ScAddress f2)
        {
            length = 0;
            bytes = new byte[0];
            arguments = new List<IArgument>();

            templateType = ConstructionTemplateType.t_3F_A_F;
            arguments.Add(new Argument<ScAddress>(f1));
            arguments.Add(new Argument<ElementType>(a1));
            arguments.Add(new Argument<ScAddress>(f2));

            createByteStream();
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

            createByteStream();
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

            createByteStream();
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

            createByteStream();
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

            createByteStream();
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

            createByteStream();
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

            createByteStream();
        }

        private void createByteStream()
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
