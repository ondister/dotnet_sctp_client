﻿namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Запрос всех произошедших событий.
    /// </summary>
    ///  /// <remarks>Стоит заметить, что команда может не вернуть события, произошедшие за 1 секунду до ее вызова. Поэтому, делайте таймаут перед вызовом этой команды в 1 секунду.</remarks>
    public class EmitEventsCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        ///         /// <remarks>Стоит заметить, что команда может не вернуть события, произошедшие за 1 секунду до ее вызова. Поэтому, делайте таймаут перед вызовом этой команды в 1 секунду.</remarks>        
        public EmitEventsCommand()
            : base(CommandCode.EmitEvents)
        { }
    }
}
