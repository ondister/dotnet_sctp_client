namespace Ostis.Sctp
{
    /// <summary>
    /// Тип события.
    /// </summary>
    public enum EventType : byte
    {
       
        /// <summary>
        /// Добавление исходящей дуги (ребра).
        /// </summary>
        AddOutArc = 0,

        /// <summary>
        /// Добавление входящей дуги (ребра).
        /// </summary>
        AddInArc = 1,

        /// <summary>
        /// Удаление исходящей дуги (ребра).
        /// </summary>
        DeleteOutArc = 2,

        /// <summary>
        /// Удаление входящей дуги (ребра).
        /// </summary>
        DeleteInArc = 3,

        /// <summary>
        /// Удаление sc-элемента.
        /// </summary>
        DeleteElement = 4,

         /// <summary>
        /// Изменение содержимого ссылки
        /// </summary>
        LinkContentChange= 5
    
    }
}
