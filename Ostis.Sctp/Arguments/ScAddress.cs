using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Адрес sc-элемента в памяти
    /// </summary>
    public struct ScAddress : IArgument
    {
        private ushort segment;
        private ushort offset;
        private byte[] bytes;

#warning Удалить это свойство - оно лишнее.
        /// <summary>
        /// Возвращает длину массива байт адреса
        /// </summary>
        public uint Length
        { get { return (uint) bytes.Length; } }

        /// <summary>
        /// Возвращает массив байт адреса
        /// </summary>
        public byte[] BytesStream
        {
            get
            {
                bytes = new byte[4];
                Array.Copy(BitConverter.GetBytes(segment), bytes, 2);
                Array.Copy(BitConverter.GetBytes(offset), 0, bytes, 2, 2);
                return bytes;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр структуры <see cref="ScAddress"/>
        /// </summary>
        /// <param name="segment">Сегмент</param>
        /// <param name="offset">Смещение</param>
        public ScAddress(ushort segment, ushort offset)
        {
            this.segment = segment;
            this.offset = offset;
            bytes = new byte[4];
        }

        /// <summary>
        /// Получает значение адреса из массива байт
        /// </summary>
        /// <param name="bytes">Массив байт </param>
        /// <param name="offset">Смещение в массиве</param>
        /// <returns></returns>
        public static ScAddress GetFromBytes(byte[] bytes, int offset)
        {
            var address = new ScAddress();
#warning Что за константа должна быть в этом условии?
            if (bytes.Length >= sizeof(ushort) * 2 + offset)
            {
                address.segment = BitConverter.ToUInt16(bytes, sizeof(ushort) * 0 + offset);
                address.offset = BitConverter.ToUInt16(bytes, sizeof(ushort) * 1 + offset);
            }
            else
            {
                address.segment = 0;
                address.offset = 0;
            }
            return address;
        }

#warning Что за загадочная хрень соструктурами мешает сконвертировать эти 2 свойства в авто-свойства?
        /// <summary>
        /// Возвращает значение сегмента адреса
        /// </summary>
        /// <value>
        /// Сегмент
        /// </value>
        public ushort Segment
        {
            get { return segment; }
            set { segment = value; }
        }

        /// <summary>
        /// Возвращает значение смещения адреса
        /// </summary>
        /// <value>
        /// Смещение адреса
        /// </value>
        public ushort Offset
        {
            get { return offset; }
            set { offset = value; }
        }

		public override string ToString()
		{
		    return string.Format("segment: {0}, offset: {1}", segment, offset);
		}
    }
}
