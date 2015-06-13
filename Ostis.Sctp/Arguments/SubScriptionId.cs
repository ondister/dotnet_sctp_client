using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Sub scription identifier.
	/// </summary>
	public struct SubscriptionId : IArgument
	{
		private int id;
		private readonly byte[] bytes;

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
		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="id">идентификатор.</param>
		public SubscriptionId(int id)
		{
			this.id = id;
            bytes = new byte[4];
		}

		/// <summary>
		/// Получение числа из массива байт.
		/// </summary>
        /// <param name="bytes">массив байт</param>
        /// <returns>число</returns>
		public static int GetFromBytes(byte[] bytes)
		{
			return bytes.Length >= sizeof(int) ? BitConverter.ToInt32(bytes, sizeof(int)) : 0;
		}
	}
}

