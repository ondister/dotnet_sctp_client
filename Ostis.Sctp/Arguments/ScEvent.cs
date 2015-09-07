using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Событие в SC-памяти.
	/// </summary>
    /// /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="ScEvent"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="ScEvent" lang="C#" />
    /// </example>
	public class ScEvent : IArgument
	{
        private readonly SubscriptionId subscriptionId;
		private readonly ScAddress elementAddress;
		private readonly ScAddress arcAddress;

	    /// <summary>
	    /// ID подписки.
	    /// </summary>
	    public SubscriptionId SubscriptionId
	    { get { return subscriptionId; } }

	    /// <summary>
	    /// Адрес элемента.
	    /// </summary>
	    public ScAddress ElementAddress
	    { get { return elementAddress; } }

	    /// <summary>
	    /// Адрес дуги.
	    /// </summary>
	    public ScAddress ArcAddress
	    { get { return arcAddress; } }

	    /// <summary>
		/// Инициализирует новое событие.
		/// </summary>
        /// <param name="subscriptionId">id подписки</param>
        /// <param name="elementAddress">адрес элемента</param>
        /// <param name="arcAddress">адрес дуги</param>
        public ScEvent(SubscriptionId subscriptionId, ScAddress elementAddress, ScAddress arcAddress)
		{
            this.subscriptionId = subscriptionId;
            this.elementAddress = elementAddress;
            this.arcAddress = arcAddress;
		}

	    /// <summary>
		/// Получает значение события из массива байт.
		/// </summary>
		/// <param name="bytes">массив байт</param>
		/// <param name="offset">смещение в массиве</param>
		/// <returns></returns>
		public static ScEvent Parse(byte[] bytes, int offset)
	    {
	        return bytes.Length >= SctpProtocol.ScEventLength + offset
                ? new ScEvent(
                    SubscriptionId.Parse(bytes, offset),
                    ScAddress.Parse(bytes, offset + SctpProtocol.SubscriptionIdLength),
                    ScAddress.Parse(bytes, offset + SctpProtocol.SubscriptionIdLength + SctpProtocol.ScAddressLength))
                : null;
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new List<byte>();
            bytes.AddRange(subscriptionId.GetBytes());
            bytes.AddRange(elementAddress.GetBytes());
            bytes.AddRange(arcAddress.GetBytes());
            return bytes.ToArray();
        }

        #endregion
	}
}

