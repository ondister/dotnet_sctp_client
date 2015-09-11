using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Цепочка итераторов. Это аргумент команды <see cref="Ostis.Sctp.Commands.IterateConstructionsCommand"/>
    /// </summary>
    public class IteratorsChain : IArgument
    {
        private List<IteratorsChainMember> chainMembers;
        private byte[] byter;

        /// <summary>
        /// Возвращает или задает набор звеньев <see cref="IteratorsChainMember"/> цепочки итераторов
        /// </summary>
        /// <value>
        /// Звено <see cref="IteratorsChainMember"/> цепочки итераторов
        /// </value>
        public List<IteratorsChainMember> ChainMembers
        {
            get { return chainMembers; }
            set { chainMembers = value; }
        }
        private readonly ConstructionTemplate initialConstruction;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IteratorsChain"/>
        /// </summary>
        /// <param name="initialTemplate">Первый шаблон цепочки</param>
        public IteratorsChain(ConstructionTemplate initialTemplate)
        {
            chainMembers = new List<IteratorsChainMember>();
            initialConstruction = initialTemplate;
        }

        #region Реализация интерфеса IArgument
        //t_3F_A_A  [255][t][t]
        //t_3A_A_F [255][t][adr]
        //t_3F_A_F  [255][255][adr][type][adr]
        //t_5F_A_A_A_F [255][255][adr][t][t][t][adr]
        //t_5A_A_F_A_F [255][255][t][t][adr][t][adr]
        //t_5F_A_F_A_F [255][255][255][adr][t][adr][t][adr]
        //t_5F_A_F_A_A [255][255][adr][t][adr][t][t]
        //t_5F_A_A_A_A [255][adr][t][t][t][t]
        //t_5A_A_F_A_A [255][t][t][adr][t][t]
        //делаем шаблоны и подставляем их элементы друг в друга. 
        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {

            using (var stream = new MemoryStream())
            {
                //добавить число итераторов
                byte iteratorsCount = Convert.ToByte(chainMembers.Count + 1);
                stream.WriteByte(iteratorsCount);
                //добавить начальный
                var bytes = initialConstruction.GetBytes();
                stream.Write(bytes, 0, bytes.Length);
                //добавить все остальные
                int memberIndex = 1;
                foreach (var argument in chainMembers)
                {
                    bytes = argument.GetBytes();
                    bytes[findFIndex(argument)] = Convert.ToByte((int)argument.Substitution.FirstIteratorElementIndex * memberIndex);
                    stream.Write(bytes, 0, bytes.Length);
                    memberIndex++;

                }
                byter = stream.ToArray();
                return stream.ToArray();
            }


        }
        /// <summary>
        /// ищем какой это f по счету на основании инджекса элемента
        /// </summary>
        private int findFIndex(IteratorsChainMember chainMember)
        {
            int fIndex = 1;//индексация начинается с 1 (то есть 1 или 2 или 3 f)
            for (int elementindex = 0; elementindex < chainMember.Substitution.NextIteratorElementIndex;elementindex++ )
            {
                if(chainMember.ConstructionTemplate.Elements[elementindex].GetType()==typeof(ScAddress)){fIndex++;}
            }

            return fIndex;
        }
        #endregion
    }
}
