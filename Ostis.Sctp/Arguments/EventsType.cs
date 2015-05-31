namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Тип события .
    /// </summary>
#warning Переиметовать члены перечисления.
    public enum EventsType : byte
    {
        /// <summary>
        /// Добавление исходящей дуги (ребра).
        /// </summary>
        ArcOutAdd = 0,

        /// <summary>
        /// Добавление входящей дуги (ребра).
        /// </summary>
        ArcInAdd = 1,

        /// <summary>
        /// Удаление исходящей дуги (ребра).
        /// </summary>
        ArcOutDel = 2,

        /// <summary>
        /// Удаление входящей дуги (ребра).
        /// </summary>
        ArcInDel = 3,

        /// <summary>
        /// Удаление sc-элемента.
        /// </summary>
        ElementDel = 4,
    }
}
