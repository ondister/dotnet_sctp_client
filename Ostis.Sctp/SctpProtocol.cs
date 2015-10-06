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
        /// Адрес сервера по умолчанию.
        /// </summary>
        public const string TestServerIp = "127.0.0.1";

        /// <summary>
        /// Номер порта по умолчанию.
        /// </summary>
        public const int DefaultPortNumber = 55770;

        /// <summary>
        /// Длина SCTP-заголовка.
        /// </summary>
        public const int HeaderLength = sizeof(byte) + sizeof(uint) + sizeof(byte) + sizeof(uint); // Result Code, ID, Return Code, Return Size

        #region Длины передаваемых данных

        /// <summary>
        /// Размер данных SC-адреса.
        /// </summary>
        public const int ScAddressLength = sizeof(ushort) * 2; // Segment : Offset

        /// <summary>
        /// Размер данных ID подписки.
        /// </summary>
        public const int SubscriptionIdLength = sizeof(int); // ID

        /// <summary>
        /// Размер данных SC-события.
        /// </summary>
        public const int ScEventLength = SubscriptionIdLength + ScAddressLength*2; // Subscription ID + Element + Arc

        /// <summary>
        /// Размер данных структуры статистики.
        /// </summary>
        public const int StatisticsDataLength = sizeof(ulong) * 11 + sizeof(byte); // 1 UnixDate (ulong) + 10 x raw ulong + 1 bool (byte)

        #endregion

        /// <summary>
        /// Кодировка текста по умолчанию.
        /// </summary>
        public static readonly Encoding TextEncoding = new UTF8Encoding();
    }
}
