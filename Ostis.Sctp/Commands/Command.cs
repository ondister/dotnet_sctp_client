using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    public static class CommandFactory
    {
#warning Этот класс не нужен
        /// <summary>
        /// Проверка существования элемента с указанным sc-адресом
        /// </summary>
        /// <param name="address">sc-адрес проверяемого sc-элемента</param>
        /// <returns></returns>
        public static Command CheckElement(ScAddress address)
        {
            return new CheckElementCommand(address);
        }

        /// <summary>
        /// Создание новой sc-дуги указанного типа, с указнным начальным и конечным элементами
        /// </summary>
        /// <param name="arcType">тип создаваемой sc-дуги</param>
        /// <param name="beginAddress">sc-адрес начального элемента sc-дуги</param>
        /// <param name="endAddress">sc-адрес конечного элемента sc-дуги</param>
        /// <returns></returns>
        public static Command CreateArc(ElementType arcType, ScAddress beginAddress, ScAddress endAddress)
        {
            return new CreateArcCommand(arcType, beginAddress, endAddress);
        }

        /// <summary>
        /// Создание новой sc-ссылки
        /// </summary>
        /// <returns></returns>
        public static Command CreateLink()
        {
            return new CreateLinkCommand();
        }

        /// <summary>
        /// Создание нового sc-узла указанного типа
        /// </summary>
        /// <param name="type">тип создаваемого sc-узла</param>
        /// <returns></returns>
        public static Command CreateNode(ElementType type)
        {
            return new CreateNodeCommand(type);
        }

        /// <summary>
        /// Удаление sc-элемента с указанным sc-адресом
        /// </summary>
        /// <param name="address"> sc-адрес удаляемого sc-элемента</param>
        /// <returns></returns>
        public static Command DeleteElement(ScAddress address)
        {
            return new DeleteElementCommand(address);
        }

        /// <summary>
        /// Поиск sc-элемента по его системному идентификатору
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns></returns>
        public static Command FindElementById(Identifier identifier)
        {
            return new FindElementCommand(identifier);
        }

        /// <summary>
        /// Поиск всех sc-ссылок с указанным содержимым
        /// </summary>
        /// <param name="content">содержимое для поиска </param>
        /// <returns></returns>
        public static Command FindLinks(LinkContent content)
        {
            return new FindLinksCommand(content);
        }

        /// <summary>
        /// Получение начального элемента sc-дуги
        /// </summary>
        /// <param name="address">sc-адрес дуги у которой необходимо получить начальный элемент</param>
        /// <returns></returns>
        public static Command GetArc(ScAddress address)
        {
            return new GetArcCommand(address);
        }

        /// <summary>
        /// Получение типа sc-элемента по sc-адресу
        /// </summary>
        /// <param name="address">sc-адрес элемента для получения типа</param>
        /// <returns></returns>
        public static Command GetElementType(ScAddress address)
        {
            return new GetElementTypeCommand(address);
        }

        /// <summary>
        /// Получение содержимого sc-ссылки
        /// </summary>
        /// <param name="address">sc-адрес ссылки для получения содержимого</param>
        /// <returns></returns>
        public static Command GetLinkContent(ScAddress address)
        {
            return new GetLinkContentCommand(address);
        }

        /// <summary>
        /// Получение статистики с сервера, в ременных границах.
        /// </summary>
        /// <param name="startTime">Нижняя временная граница</param>
        /// <param name="endTime">Верхняя временная граница</param>
        /// <returns></returns>
        public static Command GetStatistics(UnixDateTime startTime, UnixDateTime endTime)
        {
            return new GetStatisticsCommand(startTime, endTime);
        }

        /// <summary>
        /// Найти конструкции по указанному 3-х или 5-ти элементному шаблону
        /// </summary>
        /// <param name="template">Шаблон для поиска</param>
        /// <returns></returns>
        public static Command IterateElements(ConstructionTemplate template)
        {
            return new IterateElementsCommand(template);
        }

        /// <summary>
        /// Установка содержимого sc-ссылки
        /// </summary>
        /// <param name="address">sc-адрес ссылки </param>
        /// <param name="linkcontent">данные устанавливаемого содержимого</param>
        /// <returns></returns>
        public static Command SetLinkContent(ScAddress address, LinkContent linkcontent)
        {
            return new SetLinkContentCommand(address, linkcontent);
        }

        /// <summary>
        /// Установка системного идентификатора sc-элемента
        /// </summary>
        /// <param name="address"> адрес sc-эелемента </param>
        /// <param name="identifier">Идентификатор</param>
        /// <returns></returns>
        public static Command SetSysId(ScAddress address, Identifier identifier)
        {
            return new SetSystemIdCommand(address, identifier);
        }

        /// <summary>
        /// Получение версии протокола
        /// </summary>
        /// <returns></returns>
        public static Command GetProtocolVersion()
        {
            return new GetProtocolVersionCommand();
        }

        /// <summary>
        /// Создание подписки на событие
        /// </summary>
        /// <returns></returns>
        public static Command CreateEventSubscription(EventsType type, ScAddress address)
        {
            return new CreateSubscriptionCommand(type, address);
        }

        /// <summary>
        /// Удаление подписки на событие
        /// </summary>
        /// <returns></returns>
        public static Command DeleteEventSubscription(SubscriptionId id)
        {
            return new DeleteSubscriptionCommand(id);
        }

        /// <summary>
        /// Запрос всех произошдших событий
        /// </summary>
        /// <returns></returns>
        public static Command EmitEvents()
        {
            return new EmitEventsCommand();
        }
    }
}
