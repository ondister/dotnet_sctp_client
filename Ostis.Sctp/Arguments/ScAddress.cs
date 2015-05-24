using System;

namespace Ostis.Sctp.Arguments
{

    /// <summary>
    /// Адрес sc-элемента в памяти
    /// </summary>
    public struct ScAddress:IArgument
    {
        private ushort _segment;
        private ushort _offset;
        private byte[] _bytestream;

        /// <summary>
        /// Возвращает длину массива байт адреса
        /// </summary>
        public uint Length
        {
            get { return (uint)_bytestream.Length; }
        }

        /// <summary>
        /// Возвращает массив байт адреса
        /// </summary>
        public byte[] BytesStream
        {
            get
            {
                _bytestream = new byte[4];
                Array.Copy(BitConverter.GetBytes(_segment), _bytestream, 2);
                Array.Copy(BitConverter.GetBytes(_offset), 0, _bytestream, 2, 2);
                return _bytestream;
            }
        }


        /// <summary>
        /// Инициализирует новый экземпляр структуры <see cref="ScAddress"/>
        /// </summary>
        /// <param name="segment">Сегмент</param>
        /// <param name="offset">Смещение</param>
        public ScAddress(ushort segment, ushort offset)
        {
            _segment = segment;
            _offset = offset;
            _bytestream = new byte[4];

        }


        /// <summary>
        /// Получает значение адреса из массива байт
        /// </summary>
        /// <param name="bytesstream">Массив байт </param>
        /// <param name="offset">Смещение в массиве</param>
        /// <returns></returns>
        public static ScAddress GetFromBytes(byte[] bytesstream, int offset)
        {
            ScAddress tmpaddr = new ScAddress();
            if (bytesstream.Length >= sizeof(ushort) * 2 + offset)
            {

                tmpaddr._segment = BitConverter.ToUInt16(bytesstream, sizeof(ushort) * 0 + offset);
                tmpaddr._offset = BitConverter.ToUInt16(bytesstream, sizeof(ushort) * 1 + offset);
            }

            else
            {

                tmpaddr._segment = 0;
                tmpaddr._offset = 0;
            }
            return tmpaddr;
        }

        /// <summary>
        /// Возвращает значение сегмента адреса
        /// </summary>
        /// <value>
        /// Сегмент
        /// </value>
        public ushort Segment
        {
            get { return _segment; }
            set { _segment = value; }
        }

        /// <summary>
        /// Возвращает значение смещения адреса
        /// </summary>
        /// <value>
        /// Смещение адреса
        /// </value>
        public ushort Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

		public string ToString()
		{
			return String.Concat("segment: ",_segment.ToString(),", offset: ",_offset.ToString());
		}

       
    }
}
