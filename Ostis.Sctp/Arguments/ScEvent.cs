using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Событие в SC-памяти.
	/// </summary>
	public struct ScEvent : IArgument
	{
        private SubscriptionId subscriptionId;
		private ScAddress elementAddress;
		private ScAddress arcAddress;

	    /// <summary>
	    /// ID подписки.
	    /// </summary>
	    public SubscriptionId Id
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
		/// ctor.
		/// </summary>
        /// <param name="subscriptionId">id подписки</param>
        /// <param name="elementAddress">адрес элемента</param>
        /// <param name="arcAddress">адрес дуги</param>
        public ScEvent(SubscriptionId subscriptionId, ScAddress elementAddress, ScAddress arcAddress)
		{
            this.subscriptionId = subscriptionId;
            this.elementAddress = elementAddress;
            this.arcAddress = arcAddress;
#warning Magic number 12
            //this.bytes = new byte[12];
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
                    SubscriptionId.Parse(bytes, sizeof(uint) * 0 + offset),
                    ScAddress.Parse(bytes, sizeof(uint) * 1 + offset),
                    ScAddress.Parse(bytes, sizeof(uint) * 2 + offset))
                : new ScEvent(new SubscriptionId(), new ScAddress(), new ScAddress());
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

