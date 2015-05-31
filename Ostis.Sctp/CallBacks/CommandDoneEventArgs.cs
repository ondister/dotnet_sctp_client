using System;

namespace Ostis.Sctp.CallBacks
{
    /// <summary>
    /// Делегат события завершения выполнения команды.
    /// </summary>
    /// <param name="sender">команда</param>
    /// <param name="arg">данные события</param>
    public delegate void CommandDoneEventHandler(Command sender, CommandDoneEventArgs arg);

    /// <summary>
    /// Данные события завершения исполнения команды.
    /// </summary>
    public class CommandDoneEventArgs : EventArgs
    {
        /// <summary>
        /// Код завешения.
        /// </summary>
        public ReturnCode ReturnCode 
        { get; set; }
    }
}
