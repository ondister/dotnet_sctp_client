using System;

using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение типа SC-элемента по SC-адресу.
    /// </summary>
    public class GetElementTypeCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес элемента для получения типа</param>
        public GetElementTypeCommand(ScAddress address)
            : base(0x02, 0)
        {
            UInt32 argumentsSize = 0;
            Arguments.Add(new Argument<ScAddress>(address));
            foreach (var argument in Arguments)
            {
                argumentsSize += argument.Length;
            }
            Header.ArgumentsSize = argumentsSize;
        }
    }
}
