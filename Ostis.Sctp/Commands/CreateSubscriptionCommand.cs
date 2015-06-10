using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание подписки на события.
    /// </summary>
    public class CreateSubscriptionCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="type">тип события</param>
        /// <param name="address">SC-адрес</param>
		public CreateSubscriptionCommand(EventsType type, ScAddress address)
            : base(0x0e ,0)
        {
            uint argumentsSize = 0; 
            Arguments.Add (new Argument<EventsType>(type));
			Arguments.Add(new Argument<ScAddress>(address));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }


    }
}
