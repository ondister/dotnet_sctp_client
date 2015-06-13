using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
    /// <summary>
    /// Команда: Создание новой SC-дуги указанного типа, с указнным начальным и конечным элементами.
    /// </summary>
    public class CreateArcCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Тип создаваемой SC-дуги.
        /// </summary>
        public ElementType ArcType
        { get { return arcType; } }

        /// <summary>
        /// SC-адрес начального элемента SC-дуги.
        /// </summary>
        public ScAddress BeginAddress
        { get { return beginAddress; } }

        /// <summary>
        /// SC-адрес конечного элемента SC-дуги.
        /// </summary>
        public ScAddress EndAddress
        { get { return endAddress; } }

        private readonly ElementType arcType;
        private readonly ScAddress beginAddress;
        private readonly ScAddress endAddress;

        #endregion

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="arcType">тип создаваемой SC-дуги</param>
        /// <param name="beginAddress">SC-адрес начального элемента SC-дуги</param>
        /// <param name="endAddress">SC-адрес конечного элемента SC-дуги</param>
        public CreateArcCommand(ElementType arcType, ScAddress beginAddress, ScAddress endAddress)
            : base(CommandCode.CreateArc)
        {
            Arguments.Add(new ElementTypeArgument(this.arcType = arcType));
            Arguments.Add(this.beginAddress = beginAddress);
            Arguments.Add(this.endAddress = endAddress);
        }
    }
}
