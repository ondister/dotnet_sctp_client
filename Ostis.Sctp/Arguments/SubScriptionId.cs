using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Sub scription identifier.
	/// </summary>
	public struct SubscriptionId : IArgument
	{
		private int id;

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
		/// <param name="id">идентификатор</param>
		public SubscriptionId(int id)
		{
			this.id = id;
		}

		/// <summary>
		/// Получение числа из массива байт.
		/// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        /// <returns>число</returns>
        public static SubscriptionId Parse(byte[] bytes, int offset)
		{
            return new SubscriptionId(bytes.Length >= sizeof(int) ? BitConverter.ToInt32(bytes, sizeof(int) + offset) : 0);
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(id);
        }

        #endregion
	}
}

