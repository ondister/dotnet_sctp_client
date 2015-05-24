using System.Collections.Generic;
using System.IO;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Шаблон поиска конструкции для команды CmdGetIterateElements
    /// </summary>
    
    public struct ConstrTemplate : IArgument
    {
        private List<IArgument> _args;
        private byte[] _bytes;
        private uint _lenght;
        private ConstrTemplateType _templtype;

        internal ConstrTemplateType Type
        {
            get { return _templtype; }
        }

        /// <summary>
        /// Возвращает длину массива байт шаблона
        /// </summary>
        public uint Length
        {
            get { return _lenght; }
        }

        /// <summary>
        /// Возвращает массив байт шаблона
        /// </summary>
        public byte[] BytesStream
        {
            get { return _bytes; }
        }

        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска исходящих дуг из указанного элемента
        /// </summary>
        /// <param name="f">Адрес начального sc-элемента</param>
        /// <param name="a1">Тип исходящей дуги дуги</param>
        /// <param name="a2">Тип конечного sc-элемента (дуги или узла)</param>
        public ConstrTemplate(ScAddress f, ElementType a1, ElementType a2)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_3F_A_A;
            _args.Add(new Argument<ScAddress>(f));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска входящих дуг к указанному элементу.
        /// </summary>
        /// <param name="a1">Тип начального sc-элемента</param>
        /// <param name="a2">Тип входящей дуги</param>
        /// <param name="f">Адрес конечного sc-элемента</param>
        public ConstrTemplate(ElementType a1, ElementType a2, ScAddress f)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_3A_A_F;
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ScAddress>(f));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из трех sc-элементов для поиска дуги между двумя элементами.
        /// </summary>
        /// <param name="f1">Адрес начального sc-элемента</param>
        /// <param name="a1">Тип искомой дуги</param>
        /// <param name="f2">Адрес конечного sc-элемента</param>
        public ConstrTemplate(ScAddress f1, ElementType a1, ScAddress f2)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_3F_A_F;
            _args.Add(new Argument<ScAddress>(f1));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ScAddress>(f2));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="a1">Тип первого sc-элемента конструкции</param>
        /// <param name="a2">Тип второго sc-элемента конструкции</param>
        /// <param name="f">Адрес третьего sc-элемента</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">Тип пятого sc-элемента конструкции</param>
        public ConstrTemplate(ElementType a1, ElementType a2, ScAddress f, ElementType a3, ElementType a4)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5A_A_F_A_A;
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ScAddress>(f));
            _args.Add(new Argument<ElementType>(a3));
            _args.Add(new Argument<ElementType>(a4));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="a1">Тип первого sc-элемента конструкции</param>
        /// <param name="a2">Тип второго sc-элемента конструкции</param>
        /// <param name="f1">Адрес третьего sc-элемента</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">Адрес пятого sc-элемента</param>
        public ConstrTemplate(ElementType a1, ElementType a2, ScAddress f1, ElementType a3, ScAddress f2)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5A_A_F_A_F;
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ScAddress>(f1));
            _args.Add(new Argument<ElementType>(a3));
            _args.Add(new Argument<ScAddress>(f2));


            this.CreateByteStream();
        }

        /// <summary>
        ///  Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="a2">Тип третьего sc-элемента конструкции</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="a4">Тип пятого sc-элемента конструкции</param>
        public ConstrTemplate(ScAddress f, ElementType a1, ElementType a2, ElementType a3, ElementType a4)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5F_A_A_A_A;
            _args.Add(new Argument<ScAddress>(f));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ElementType>(a3));
            _args.Add(new Argument<ElementType>(a4));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f1">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="a2">Тип третьего sc-элемента конструкции</param>
        /// <param name="a3">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f2">Адрес пятого sc-элемента</param>
        public ConstrTemplate(ScAddress f1, ElementType a1, ElementType a2, ElementType a3, ScAddress f2)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5F_A_A_A_F;
            _args.Add(new Argument<ScAddress>(f1));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ElementType>(a3));
            _args.Add(new Argument<ScAddress>(f2));


            this.CreateByteStream();
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
        public ConstrTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ElementType a3)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5F_A_F_A_A;
            _args.Add(new Argument<ScAddress>(f1));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ScAddress>(f2));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ElementType>(a3));


            this.CreateByteStream();
        }

        /// <summary>
        /// Инициализирует шаблон из пяти sc-элементов.
        /// </summary>
        /// <param name="f1">Адрес первого sc-элемента</param>
        /// <param name="a1">Тип второго sc-элемента конструкции</param>
        /// <param name="f2">Адрес третьего sc-элемента</param>
        /// <param name="a2">Тип четвертого sc-элемента конструкции</param>
        /// <param name="f3">Адрес пятого sc-элемента</param>
        public ConstrTemplate(ScAddress f1, ElementType a1, ScAddress f2, ElementType a2, ScAddress f3)
        {
            _lenght = 0;
            _bytes = new byte[0];
            _args = new List<IArgument>();

            _templtype = ConstrTemplateType.t_5F_A_F_A_F;
            _args.Add(new Argument<ScAddress>(f1));
            _args.Add(new Argument<ElementType>(a1));
            _args.Add(new Argument<ScAddress>(f2));
            _args.Add(new Argument<ElementType>(a2));
            _args.Add(new Argument<ScAddress>(f3));


            this.CreateByteStream();
        }

        private void CreateByteStream()
        {
            MemoryStream mstream = new MemoryStream();
            mstream.Write(new byte[] { (byte)_templtype }, 0, 1);
            foreach (IArgument arg in _args)
            {
                mstream.Write(arg.BytesStream, 0, (int)arg.Length);
            }
            _bytes = mstream.ToArray();
            _lenght = (uint)_bytes.Length;
        }


    }
}
