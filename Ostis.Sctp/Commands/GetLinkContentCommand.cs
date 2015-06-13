using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Получение содержимого SC-ссылки.
    /// </summary>
    public class GetLinkContentCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="address">SC-адрес ссылки для получения содержимого</param>
        public GetLinkContentCommand(ScAddress address)
            : base(CommandCode.GetLinkContent, 0)
        {
            Arguments.Add(new Argument<ScAddress>(address));
        }
    }
}
