namespace Ostis.Sctp
{
    /// <summary>
    /// Интерфейс аргумента команды.
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        byte[] GetBytes();
    }
}
