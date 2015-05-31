using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Событие в sc памяти
	/// </summary>
	public struct ScEvent : IArgument
	{
        private SubscriptionId subscriptionId;
		private ScAddress elementAddress;
		private ScAddress arcAddress;
		private byte[] bytes;

	    /// <summary>
	    /// Gets the ID
	    /// </summary>
	    /// <value>The I.</value>
	    public SubscriptionId ID
	    { get { return subscriptionId; } }

	    /// <summary>
	    /// Gets the element address.
	    /// </summary>
	    /// <value>The element address.</value>
	    public ScAddress ElementAddress
	    { get { return elementAddress; } }

	    /// <summary>
	    /// Gets the arc address.
	    /// </summary>
	    /// <value>The arc address.</value>
	    public ScAddress ArcAddress
	    { get { return arcAddress; } }

	    /// <summary>
		/// Initializes a new instance of the <see cref="sctp_client.Arguments.ScEvent"/> struct.
		/// </summary>
        /// <param name="subscriptionId">ID</param>
        /// <param name="elementAddress">Element address.</param>
        /// <param name="arcAddress">Arc address.</param>
        public ScEvent(SubscriptionId subscriptionId, ScAddress elementAddress, ScAddress arcAddress)
		{
            this.subscriptionId = subscriptionId;
            this.elementAddress = elementAddress;
            this.arcAddress = arcAddress;
#warning Magic number 12
            this.bytes = new byte[12];
		}

	    /// <summary>
	    /// Возвращает длину массива байт аргумента
	    /// </summary>
	    /// <value>The length.</value>
	    public uint Length
	    { get { return (uint) bytes.Length; } }

	    /// <summary>
	    /// Возвращает массив байт аргумента
	    /// </summary>
	    /// <value>The bytes stream.</value>
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
		/// Получает значение события из массива байт
		/// </summary>
		/// <param name="bytesstream">Массив байт </param>
		/// <param name="offset">Смещение в массиве</param>
		/// <returns></returns>
		public static ScEvent GetFromBytes(byte[] bytesstream, int offset)
	    {
	        return bytesstream.Length >= sizeof(Int32) * 3 + offset
                ? new ScEvent(
                    new SubscriptionId(BitConverter.ToInt32(bytesstream, sizeof(UInt32) * 0 + offset)),
                    ScAddress.GetFromBytes(bytesstream, sizeof(UInt32) * 1 + offset),
                    ScAddress.GetFromBytes(bytesstream, sizeof(UInt32) * 2 + offset))
                : new ScEvent(new SubscriptionId(), new ScAddress(), new ScAddress());
		}
	}
}

