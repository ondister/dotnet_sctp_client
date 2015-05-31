using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Sub scription identifier.
	/// </summary>
	public struct SubscriptionId : IArgument
	{
		private Int32 id;
		private readonly byte[] bytes;

#warning Удалить это свойство - оно лишнее.
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
	            Array.Copy(BitConverter.GetBytes(id), bytes, 4);
	            return bytes;
	        }
	    }

#warning Что за загадочная хрень соструктурами мешает сконвертировать эти 2 свойства в авто-свойства?
        /// <summary>
		/// Gets or sets the I.
		/// </summary>
		/// <value>The I.</value>
		public Int32 ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="sctp_client.Arguments.SubScriptionId"/> struct.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public SubscriptionId(Int32 id)
		{
			this.id = id;
            this.bytes = new byte[4];
		}

		/// <summary>
		/// Gets from bytes.
		/// </summary>
		/// <returns>The from bytes.</returns>
		/// <param name="bytes">Bytesstream.</param>
		public static Int32 GetFromBytes(byte[] bytes)
		{
			return bytes.Length >= sizeof(Int32) ? BitConverter.ToInt32(bytes, sizeof(Int32)) : 0;
		}
	}
}

