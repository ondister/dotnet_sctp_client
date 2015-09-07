using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Аргумент типа события.
    /// </summary>
    internal class EventTypeArgument : IArgument
    {
        private EventType eventType;

        /// <summary>
		/// Тип события.
		/// </summary>
		public EventType EventType
		{
			get { return eventType; }
			set { eventType = value; }
		}

		/// <summary>
		/// Инициализирует новое событие указанного типа.
		/// </summary>
        /// <param name="eventType">тип события</param>
        public EventTypeArgument(EventType eventType)
		{
            this.eventType = eventType;
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[1];
            bytes[0] = (byte)eventType;
            return bytes;
        }

        #endregion
    }
}
