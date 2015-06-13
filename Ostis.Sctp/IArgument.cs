namespace Ostis.Sctp
{
    /// <summary>
    /// Аргумент команды.
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        byte[] GetBytes();
    }
}
