namespace Ostis.Sctp
{
    /// <summary>
    /// Код (тип) команды.
    /// </summary>
    public enum CommandCode : byte
    {
        /// <summary>
        /// Тип команды неизвестен.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Проверка существования элемента с указанным SC-адресом.
        /// </summary>
        CheckElement = 0x01,

        /// <summary>
        /// Получение типа SC-элемента по SC-адресу.
        /// </summary>
        GetElementType = 0x02,

        /// <summary>
        /// Удаление SC-элемента с указанным SC-адресом.
        /// </summary>
        DeleteElement = 0x03,

        /// <summary>
        /// Создание нового SC-узла указанного типа.
        /// </summary>
        CreateNode = 0x04,

        /// <summary>
        /// Создание новой SC-ссылки.
        /// </summary>
        CreateLink = 0x05,

        /// <summary>
        /// Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами.
        /// </summary>
        CreateArc = 0x06,

        /// <summary>
        /// Получение начального и конечного элемента sc-дуги
        /// </summary>
        GetArc = 0x07,

        /// <summary>
        /// Значение зарезервировано.
        /// </summary>
        Reserved = 0x08,

        /// <summary>
        /// Получение содержимого SC-ссылки.
        /// </summary>
        GetLinkContent = 0x09,

        /// <summary>
        /// Поиск всех SC-ссылок с указанным содержимым.
        /// </summary>
        FindLinks = 0x0A,

        /// <summary>
        /// Установка содержимого SC-ссылки.
        /// </summary>
        SetLinkContent = 0x0B,

        /// <summary>
        /// Поиск конструкции по указанному 3-х или 5-ти элементному шаблону.
        /// </summary>
        IterateElements = 0x0C,

        /// <summary>
        /// Итерирование конструкций.
        /// </summary>
        IterateConstructions = 0x0D,

        /// <summary>
        /// Создание подписки на события.
        /// </summary>
        CreateSubscription = 0x0E,

        /// <summary>
        /// Удаление подписки на события.
        /// </summary>
        DeleteSubscription = 0x0F,

        /// <summary>
        /// Запрос всех произошедших событий.
        /// </summary>
        EmitEvents = 0x10,

        /// <summary>
        /// Поиск SC-элемента по его системному идентификатору.
        /// </summary>
        FindElement = 0xA0,

        /// <summary>
        /// Установка системного идентификатора SC-элемента.
        /// </summary>
        SetSystemId = 0xA1,

        /// <summary>
        /// Получение статистики с сервера, во временных границах.
        /// </summary>
        GetStatistics = 0xA2,

        /// <summary>
        /// Получение версии протокола.
        /// </summary>
        GetProtocolVersion = 0xA3,

        /// <summary>
        /// Ещё одно зарегистрированное/неизвестное значение.
        /// </summary>
        UnknownReserved = 0xFE,
    }
}
