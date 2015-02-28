
namespace sctp_client
{
    /// <summary>
    /// Перечислитель типа используемого сокет клиента
    /// </summary>
   public enum ClientType
    {
        /// <summary>
        /// Асинхронный клиент
        /// </summary>
       AsyncClient=1,

       /// <summary>
       /// Синхронный клиент
       /// </summary>
       SyncClient=2
    }
}
