using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Аргумент типа события.
    /// </summary>
    public class EventsTypeArgument : IArgument
    {
        private EventsType eventsType;

        /// <summary>
		/// Тип события.
		/// </summary>
		public EventsType EventsType
		{
			get { return eventsType; }
			set { eventsType = value; }
		}

		/// <summary>
		/// Инициализирует новое событие указанного типа.
		/// </summary>
        /// <param name="eventsType">тип события</param>
        public EventsTypeArgument(EventsType eventsType)
		{
            this.eventsType = eventsType;
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[1];
            bytes[0] = (byte)eventsType;
            return bytes;
        }

        #endregion
    }
}
