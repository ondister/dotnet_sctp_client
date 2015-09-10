using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Arguments
{
    public class IteratorsChainMember : IArgument
    {
        private readonly ConstructionTemplate constructionTemplate;

        public ConstructionTemplate ConstructionTemplate
        {
            get { return constructionTemplate; }
        }

        private readonly Substitution substitution;

        public Substitution Substitution
        {
            get { return substitution; }
        }



        public IteratorsChainMember(Substitution substitution, ConstructionTemplate constructionTemplate)
        {
            this.substitution = substitution;
            this.constructionTemplate = constructionTemplate;
        }








        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
               //нужно записать сначала тип итератора, затем подстановки, затем данные итератора
                var voidbyte=(byte)255;
                var bytes = constructionTemplate.GetBytes();
                var templateTypeByte=bytes[0];
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
    }
}
