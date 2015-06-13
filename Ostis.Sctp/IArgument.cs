namespace Ostis.Sctp
{
    /// <summary>
    /// Аргумент команды.
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Массив байт.
        /// </summary>
#warning Переименовать в Bytes.
        byte[] BytesStream
        { get; }
    }
}
