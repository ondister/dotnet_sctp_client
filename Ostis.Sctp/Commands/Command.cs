using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    public static class CommandFactory
    {
        /// <summary>
        /// Проверка существования элемента с указанным sc-адресом
        /// </summary>
        /// <param name="address">sc-адрес проверяемого sc-элемента</param>
        /// <returns></returns>
        public static ACommand CheckElement(ScAddress address)
        {
            return new CmdCheckElement(address);
        }

        /// <summary>
        /// Создание новой sc-дуги указанного типа, с указнным начальным и конечным элементами
        /// </summary>
        /// <param name="arcType">тип создаваемой sc-дуги</param>
        /// <param name="beginAddress">sc-адрес начального элемента sc-дуги</param>
        /// <param name="endAddress">sc-адрес конечного элемента sc-дуги</param>
        /// <returns></returns>
        public static ACommand CreateArc(ElementType arcType, ScAddress beginAddress, ScAddress endAddress)
        {
            return new CmdCreateArc(arcType, beginAddress, endAddress);
        }

        /// <summary>
        /// Создание новой sc-ссылки
        /// </summary>
        /// <returns></returns>
        public static ACommand CreateLink()
        {
            return new CmdCreateLink();
        }

        /// <summary>
        /// Создание нового sc-узла указанного типа
        /// </summary>
        /// <param name="type">тип создаваемого sc-узла</param>
        /// <returns></returns>
        public static ACommand CreateNode(ElementType type)
        {
            return new CmdCreateNode(type);
        }

        /// <summary>
        /// Удаление sc-элемента с указанным sc-адресом
        /// </summary>
        /// <param name="address"> sc-адрес удаляемого sc-элемента</param>
        /// <returns></returns>
        public static ACommand DeleteElement(ScAddress address)
        {
            return new CmdDeleteElement(address);
        }

        /// <summary>
        /// Поиск sc-элемента по его системному идентификатору
        /// </summary>
        /// <param name="identifier">Идентификатор</param>
        /// <returns></returns>
        public static ACommand FindElementById(Identifier identifier)
        {
            return new CmdFindElementById(identifier);
        }

        /// <summary>
        /// Поиск всех sc-ссылок с указанным содержимым
        /// </summary>
        /// <param name="content">содержимое для поиска </param>
        /// <returns></returns>
        public static ACommand FindLinks(LinkContent content)
        {
            return new CmdFindLinks(content);
        }

        /// <summary>
        /// Получение начального элемента sc-дуги
        /// </summary>
        /// <param name="address">sc-адрес дуги у которой необходимо получить начальный элемент</param>
        /// <returns></returns>
        public static ACommand GetArc(ScAddress address)
        {
            return new CmdGetArc(address);
        }

        /// <summary>
        /// Получение типа sc-элемента по sc-адресу
        /// </summary>
        /// <param name="address">sc-адрес элемента для получения типа</param>
        /// <returns></returns>
        public static ACommand GetElementType(ScAddress address)
        {
            return new CmdGetElementType(address);
        }

        /// <summary>
        /// Получение содержимого sc-ссылки
        /// </summary>
        /// <param name="address">sc-адрес ссылки для получения содержимого</param>
        /// <returns></returns>
        public static ACommand GetLinkContent(ScAddress address)
        {
            return new CmdGetLinkContent(address);
        }

        /// <summary>
        /// Получение статистики с сервера, в ременных границах.
        /// </summary>
        /// <param name="startTime">Нижняя временная граница</param>
        /// <param name="endTime">Верхняя временная граница</param>
        /// <returns></returns>
        public static ACommand GetStatistics(DateTimeUNIX startTime, DateTimeUNIX endTime)
        {
            return new CmdGetStatistics(startTime, endTime);
        }

        /// <summary>
        /// Найти конструкции по указанному 3-х или 5-ти элементному шаблону
        /// </summary>
        /// <param name="template">Шаблон для поиска</param>
        /// <returns></returns>
        public static ACommand IterateElements(ConstrTemplate template)
        {
            return new CmdIterateElements(template);
        }

        /// <summary>
        /// Установка содержимого sc-ссылки
        /// </summary>
        /// <param name="address">sc-адрес ссылки </param>
        /// <param name="linkcontent">данные устанавливаемого содержимого</param>
        /// <returns></returns>
        public static ACommand SetLinkContent(ScAddress address, LinkContent linkcontent)
        {
            return new CmdSetLinkContent(address, linkcontent);
        }

        /// <summary>
        /// Установка системного идентификатора sc-элемента
        /// </summary>
        /// <param name="address"> адрес sc-эелемента </param>
        /// <param name="identifier">Идентификатор</param>
        /// <returns></returns>
        public static ACommand SetSysId(ScAddress address, Identifier identifier)
        {
            return new CmdSetSysId(address, identifier);
        }

        /// <summary>
        /// Получение версии протокола
        /// </summary>
        /// <returns></returns>
        public static ACommand GetProtocolVersion()
        {
            return new CmdGetProtocolVersion();
        }

        /// <summary>
        /// Создание подписки на событие
        /// </summary>
        /// <returns></returns>
        public static ACommand CreateEventSubscription(EventsType type, ScAddress address)
        {
            return new CmdCreateSubScription(type, address);
        }

        /// <summary>
        /// Удаление подписки на событие
        /// </summary>
        /// <returns></returns>
        public static ACommand DeleteEventSubscription(SubScriptionId id)
        {
            return new CmdDeleteSubScription(id);
        }

        /// <summary>
        /// Запрос всех произошдших событий
        /// </summary>
        /// <returns></returns>
        public static ACommand EmitEvents()
        {
            return new CmdEventsEmit();
        }
    }
}
