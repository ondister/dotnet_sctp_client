using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Sub scription identifier.
	/// </summary>
	public class SubscriptionId : IArgument
	{
		private readonly int id;

        /// <summary>
		/// Идентификатор.
		/// </summary>
		public int Id
		{ get { return id; } }

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
            return new SubscriptionId(bytes.Length >= SctpProtocol.SubscriptionIdLength ? BitConverter.ToInt32(bytes, sizeof(int) + offset) : 0);
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

