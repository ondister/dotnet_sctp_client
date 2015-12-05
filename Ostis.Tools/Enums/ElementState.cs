namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Состояние синхронизации элемента базы знаний.
    /// </summary>
    public enum ElementState : byte
    {
        /// <summary>
        /// Неизвестно (рассинхронизация).
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Синхронизирован.
        /// </summary>
        Synchronized = 1,

        /// <summary>
        /// добавлен новый (отсутствует на сервере).
        /// </summary>
        New = 2,

        /// <summary>
        /// Отредактиован существующий.
        /// </summary>
        Edited = 3,

        /// <summary>
        /// Удалён (но ещё присутствует на сервере).
        /// </summary>
        Deleted = 4,
    }
}
