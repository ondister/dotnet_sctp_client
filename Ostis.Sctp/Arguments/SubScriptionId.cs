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
	    /// Длина массива байт.
	    /// </summary>
	    public uint Length
	    { get { return (uint) bytes.Length; } }

	    /// <summary>
	    /// Массив байт.
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
		/// Идентификатор.
		/// </summary>
		public Int32 ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="id">идентификатор.</param>
		public SubscriptionId(Int32 id)
		{
			this.id = id;
            bytes = new byte[4];
		}

		/// <summary>
		/// Получение числа из массива байт.
		/// </summary>
        /// <param name="bytes">массив байт</param>
        /// <returns>число</returns>
		public static Int32 GetFromBytes(byte[] bytes)
		{
			return bytes.Length >= sizeof(Int32) ? BitConverter.ToInt32(bytes, sizeof(Int32)) : 0;
		}
	}
}

