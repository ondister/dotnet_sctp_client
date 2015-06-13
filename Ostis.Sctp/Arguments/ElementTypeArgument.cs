using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Аргумент типа элемента.
    /// </summary>
    public class ElementTypeArgument : IArgument
    {
        private ElementType elementType;

        /// <summary>
		/// Тип элемента.
		/// </summary>
        public ElementType ElementType
		{
			get { return elementType; }
			set { elementType = value; }
		}

		/// <summary>
		/// ctor.
		/// </summary>
        /// <param name="elementType">тип элемента</param>
        public ElementTypeArgument(ElementType elementType)
		{
            this.elementType = elementType;
            
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            return BitConverter.GetBytes((ushort) elementType);
        }

        #endregion
    }
}
