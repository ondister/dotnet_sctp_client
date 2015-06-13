using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Установка содержимого SC-ссылки.
    /// </summary>
    public class SetLinkContentCommand : Command
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="linkAddress">SC-адрес ссылки</param>
        /// <param name="content">данные устанавливаемого содержимого</param>
        public SetLinkContentCommand(ScAddress linkAddress, LinkContent content)
            : base(CommandCode.SetLinkContent)
        {
            Arguments.Add(linkAddress);
            Arguments.Add(content);
        }
    }
}
