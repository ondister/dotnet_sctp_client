using System;

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
		private byte[] bytes;

	    /// <summary>
	    /// ID подписки.
	    /// </summary>
	    public SubscriptionId ID
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
            this.bytes = new byte[12];
		}

	    /// <summary>
	    /// Длина массива байт.
	    /// </summary>
	    public uint Length
	    { get { return (uint) bytes.Length; } }

	    /// <summary>
	    /// Массив байт.
	    /// </summary>
	    public byte[] BytesStream
	    {
	        get
            {
#warning Magic number 12
	            bytes = new byte[12];
	            Array.Copy(subscriptionId.BytesStream, bytes, 4);
	            Array.Copy(elementAddress.BytesStream, 0, bytes, 4, 4);
	            Array.Copy(arcAddress.BytesStream, 0, bytes, 8, 4);
	            return bytes;
	        }
	    }

	    /// <summary>
		/// Получает значение события из массива байт.
		/// </summary>
		/// <param name="bytesstream">массив байт</param>
		/// <param name="offset">смещение в массиве</param>
		/// <returns></returns>
		public static ScEvent GetFromBytes(byte[] bytesstream, int offset)
	    {
	        return bytesstream.Length >= sizeof(int) * 3 + offset
                ? new ScEvent(
                    new SubscriptionId(BitConverter.ToInt32(bytesstream, sizeof(uint) * 0 + offset)),
                    ScAddress.GetFromBytes(bytesstream, sizeof(uint) * 1 + offset),
                    ScAddress.GetFromBytes(bytesstream, sizeof(uint) * 2 + offset))
                : new ScEvent(new SubscriptionId(), new ScAddress(), new ScAddress());
		}
	}
}

