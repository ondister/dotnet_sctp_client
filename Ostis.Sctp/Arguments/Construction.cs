using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
#warning См. примечание ниже: Зачем здесь этот класс?
    /// <summary>
    /// sc-конструкция, представленная в виде sc-адресов
    /// </summary>
    public class Construction
    {
        private readonly List<ScAddress> adresses;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Construction"/>
        /// </summary>
        public Construction()
        {
            adresses = new List<ScAddress>();
        }

        /// <summary>
        /// Содержит sc-адреса элементов конструкции
        /// </summary>
        public List<ScAddress> ScAdresses
        { get { return adresses; } }

#warning Зачем здесь этот метод?
        internal void AddAddress(ScAddress address)
        {
            adresses.Add(address);
        }
    }
}
