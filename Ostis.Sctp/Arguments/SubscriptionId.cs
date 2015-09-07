using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Идентификатор подписки на событие.
	/// </summary>
    /// /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="SubscriptionId"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="SubscriptionId" lang="C#" />
    /// </example>
	public class SubscriptionId : IArgument
	{
		private readonly uint id;

        /// <summary>
		/// Идентификатор.
		/// </summary>
		public uint Id
		{ get { return id; } }

		/// <summary>
		/// Инициализирует новый идентификатор подписки на событие.
		/// </summary>
		/// <param name="id">идентификатор</param>
		public SubscriptionId(uint id)
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
            return new SubscriptionId(bytes.Length >= SctpProtocol.SubscriptionIdLength ? BitConverter.ToUInt32(bytes,  offset) : 0);
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

