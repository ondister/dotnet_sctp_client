using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Звено для цепочки итераторов <see cref="IteratorsChain"/>
    /// </summary>
    /// <remarks>
    /// <para><b>Стоит запомнить, что a(assign) обозначает неизвестный элемент, для которого известен только тип  <see cref="ElementType"/>, а f(fixed)  <see cref="ScAddress"/> изввестного элемента </b></para>
    /// </remarks>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="IteratorsChainMember"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="IteratorsChain" lang="C#" />
    /// </example>
    public class IteratorsChainMember : IArgument
    {
        private readonly ConstructionTemplate constructionTemplate;

        /// <summary>
        /// Возвращает шаблон итератора <see cref="ConstructionTemplate"/> звена 
        /// </summary>
        internal ConstructionTemplate ConstructionTemplate
        {
            get { return constructionTemplate; }
        }

        private readonly Substitution substitution;

        /// <summary>
        /// Возвращает подстановку <see cref="Substitution"/> звена 
        /// </summary>
        /// <remarks>Более подробно про подстановки смотрите здесь <see cref="T:Ostis.Sctp.Commands.IterateConstractionsCommand"/></remarks>
        internal Substitution Substitution
        {
            get { return substitution; }
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IteratorsChainMember"/>
        /// </summary>
        /// <param name="substitution">Подстановка <see cref="Substitution"/></param>
        /// /// <param name="constructionTemplate">Шаблон итератора <see cref="ConstructionTemplate"/></param>
        public IteratorsChainMember(Substitution substitution, ConstructionTemplate constructionTemplate)
        {
            this.substitution = substitution;
            this.constructionTemplate = constructionTemplate;
        }


        #region Реализация интерфеса IArgument
        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                //нужно записать сначала тип итератора, затем подстановки, затем данные итератора
                var voidbyte = (byte)255;
                var bytes = GetBytesWithoutInvalidAddresses(constructionTemplate);
                var templateTypeByte = bytes[0];
                //записали тип шаблона
                stream.WriteByte(templateTypeByte);
                //пишем нулевые подстановки
                for (int felement = 0; felement < constructionTemplate.FixedCount; felement++)
                { stream.WriteByte(voidbyte); }
                //записываем данные шаблона
                stream.Write(bytes, 1, bytes.Length - 1);
                return stream.ToArray();
            }
        }

        private byte[] GetBytesWithoutInvalidAddresses(ConstructionTemplate template)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(new[] { (byte)template.Type }, 0, 1);

                foreach (var argument in template.Elements)
                {
                   
                    if (argument is ScAddress)
                    {
                        var bytes = argument.GetBytes();
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    else 
                    {
                        if ((argument as ScAddress) != ScAddress.Invalid)
                        {
                            var bytes = argument.GetBytes();
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }


                }
                return stream.ToArray();
            }

        }


        #endregion
    }
}
