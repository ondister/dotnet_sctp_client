using System.Text;

namespace Ostis.Sctp
{
    /// <summary>
    /// Настойки соединения по протоколу SCTP.
    /// </summary>
    public static class SctpProtocol
    {
        /// <summary>
        /// Размер буфера по умолчанию.
        /// </summary>
        public const int DefaultBufferSize = 1024;

        /// <summary>
        /// Номер порта по умолчанию.
        /// </summary>
        public const int DefaultPortNumber = 55770;

        /// <summary>
        /// Размер данных SC-адреса.
        /// </summary>
        public const int ScAddressLength = 4;

        /// <summary>
        /// Размер данных SC-события.
        /// </summary>
        public const int ScEventLength = 12;

        /// <summary>
        /// Размер данных структуры статистики.
        /// </summary>
        public const int StatisticsDataLength = 89;

        /// <summary>
        /// Кодировка текста по умолчанию.
        /// </summary>
        public static readonly Encoding TextEncoding = new UTF8Encoding();
    }
}
