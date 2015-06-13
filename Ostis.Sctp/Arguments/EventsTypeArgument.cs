using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Аргумент типа события.
    /// </summary>
    public class EventsTypeArgument : IArgument
    {
        private EventsType eventsType;
		private readonly byte[] bytes;

	    /// <summary>
	    /// Массив байт.
	    /// </summary>
	    public byte[] BytesStream
	    {
	        get
	        {
                Array.Copy(BitConverter.GetBytes((byte) eventsType), bytes, 1);
	            return bytes;
	        }
	    }

        /// <summary>
		/// Тип события.
		/// </summary>
		public EventsType EventsType
		{
			get { return eventsType; }
			set { eventsType = value; }
		}

		/// <summary>
		/// ctor.
		/// </summary>
        /// <param name="eventsType">тип события</param>
        public EventsTypeArgument(EventsType eventsType)
		{
            this.eventsType = eventsType;
            bytes = new byte[1];
		}
    }
}
