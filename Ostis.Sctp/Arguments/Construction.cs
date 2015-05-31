using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
#warning См. примечание ниже: Зачем здесь этот класс?
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
        internal void AddAddress(ScAddress address)
        {
            adresses.Add(address);
        }
    }
}
