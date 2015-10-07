using Ostis.Sctp.Arguments;
namespace Ostis.Sctp.Tools
{
    /// <summary>
    /// Интерфейс элемента базы знаний
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Возвращает адрес элемента
        /// </summary>
        ScAddress ScAddress { get; }

        /// <summary>
        /// Возвращает тип элемента
        /// </summary>
        ElementType Type { get; }
    }
}
