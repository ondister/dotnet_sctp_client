using System;


namespace sctp_client.CallBacks
{
    /// <summary>
    /// Делегат для события завершения выполнения команды
    /// </summary>
    /// <param name="sender">Команда</param>
    /// <param name="arg">Данные события</param>
    public delegate void CommandDoneEventHandler(ACommand sender, CommandDoneEventArgs arg);

    /// <summary>
    /// Данные события завершения исполнения команды
    /// </summary>
    public class CommandDoneEventArgs : EventArgs
    {

        /// <summary>
        /// Возвращает и задает код окончания выполнения команд
        /// </summary>
        /// <value>
        /// Код окончания выполнения команды
        /// </value>
       public enumReturnCode ReturnCode{get;set;}

    }
}
