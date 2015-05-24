using System.Collections.Generic;

namespace sctp_client.Arguments
{
    /// <summary>
    /// sc-конструкция, представленная в виде sc-адресов
    /// </summary>
    public class Construction
    {
        private List<ScAddress> _scadresses;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Construction"/>
        /// </summary>
        public Construction()
        {
            _scadresses = new List<ScAddress>();
        }

        /// <summary>
        /// Содержит sc-адреса элементов конструкции
        /// </summary>
        public List<ScAddress> ScAdresses
        {
            get { return _scadresses; }
        }

        internal void AddScAddress(ScAddress address)
        {
            _scadresses.Add(address);
        }

    }
}
