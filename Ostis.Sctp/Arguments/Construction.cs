using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
#warning См. примечание ниже: Зачем здесь этот класс?
//этот клас инкапсулирует ответы итератора, без него просто запутаемся в конструкциях ответов
    /// <summary>
    /// Sc-конструкция, представленная в виде sc-адресов.
    /// </summary>
    public class Construction
    {
        private readonly List<ScAddress> adresses;

        /// <summary>
        /// ctor.
        /// </summary>
        public Construction()
        {
            adresses = new List<ScAddress>();
        }

        /// <summary>
        /// Sc-адреса элементов конструкции.
        /// </summary>
        public List<ScAddress> ScAdresses
        { get { return adresses; } }

#warning Зачем здесь этот метод?
//метод дурацкий, можно удалять, все равно коллекция открыта
        internal void AddAddress(ScAddress address)
        {
            adresses.Add(address);
        }
    }
}
