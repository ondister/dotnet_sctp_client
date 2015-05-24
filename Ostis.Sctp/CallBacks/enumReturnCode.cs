
using System;
namespace sctp_client.CallBacks
{
    /// <summary>
    /// Коды выполнения команды
    /// </summary>
     [Flags]
   public  enum enumReturnCode:ushort
    {
        /// <summary>
        /// Успешное выполнение команды
        /// </summary>
       Successfull = 0x00,

       /// <summary>
       /// Безуспешное выполнение команды
       /// </summary>
       Unsuccessful = 0x01,

       /// <summary>
       /// Указанный sc-элемент не найден (не верный sc-адрес)
       /// </summary>
       ScAddressError = 0x02
    }
}
