using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Аргумент типа элемента.
    /// </summary>
    public class ElementTypeArgument : IArgument
    {
        private ElementType elementType;
		private readonly byte[] bytes;

	    /// <summary>
	    /// Массив байт.
	    /// </summary>
	    public byte[] BytesStream
	    {
	        get
	        {
                Array.Copy(BitConverter.GetBytes((ushort)elementType), bytes, 2);
	            return bytes;
	        }
	    }

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
            bytes = new byte[2];
		}
    }
}
