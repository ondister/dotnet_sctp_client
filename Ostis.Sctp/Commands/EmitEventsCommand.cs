namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Запрос всех произошедших событий.
    /// </summary>
    /// <remarks>
    /// <b>Стоит заметить, что команда может не вернуть события, произошедшие за 1 секунду до ее вызова. Поэтому, делайте таймаут перед вызовом этой команды в 1 секунду, или больше в зависимости от быстродействия сервера.</b>
    /// </remarks>
    /// <example>
    /// Следующий пример демонстрирует использование класса <see cref="EmitEventsCommand"/>
    /// <code source="..\Ostis.Tests\CommandsTest.cs" region="SubScriptions" lang="C#" />
    /// </example>
    public class EmitEventsCommand : Command
    {
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <remarks>Стоит заметить, что команда может не вернуть события, произошедшие за 1 секунду до ее вызова. Поэтому, делайте таймаут перед вызовом этой команды в 1 секунду или больше в зависимости от быстродействия сервера.</remarks>        
        public EmitEventsCommand()
            : base(CommandCode.EmitEvents)
        { }
    }
}
