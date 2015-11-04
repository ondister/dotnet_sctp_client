using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Адрес SC-элемента в памяти.
    /// </summary>
    /// /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="ScAddress"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="ScAddress" lang="C#" />
    /// </example>
    public class ScAddress : IArgument,IEquatable<ScAddress>
    {
        /// <summary>
        /// Возвращает неизвестный  Sc адрес
        /// </summary>
        public static readonly ScAddress Invalid = new ScAddress(0, 0);

     
        /// <summary>
        /// Возвращает известность адреса
        /// </summary>
        public bool IsValid
        {
            get
            {
               return !this.Equals(ScAddress.Invalid);
            }
        }

      


        /// <summary>
        /// Сегмент.
        /// </summary>
        public ushort Segment
        { get { return segment; } }

        /// <summary>
        /// Смещение.
        /// </summary>
        public ushort Offset
        { get { return offset; } }

        private readonly ushort segment;
        private readonly ushort offset;

        /// <summary>
        /// Инициализирует новый sc-адрес, используя смещение и сегмент.
        /// </summary>
        /// <param name="segment">сегмент</param>
        /// <param name="offset">смещение</param>
        public ScAddress(ushort segment, ushort offset)
        {
            this.segment = segment;
            this.offset = offset;
        }

        /// <summary>
        /// Получает значение адреса из массива байт.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        /// <returns>SC-адрес</returns>
        public static ScAddress Parse(byte[] bytes, int offset)
        {

            return bytes.Length >= SctpProtocol.ScAddressLength + offset
                ? new ScAddress(
                    BitConverter.ToUInt16(bytes, sizeof(ushort) * 0 + offset),
                    BitConverter.ToUInt16(bytes, sizeof(ushort) * 1 + offset))
                : null;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
		{
		    return string.Format("segment: {0}, offset: {1}", segment, offset);
		}

        #region Реализация сравнения
        /// <summary>
        /// Определяет равен ли заданный объект <see cref="ScAddress"/> текущему объекту
        /// </summary>
        /// <param name="obj">объект <see cref="ScAddress"/></param>
        public bool Equals(ScAddress obj) 
        {
            if (obj == null)
                return false;
          
            return obj.Offset == this.Offset && obj.Segment== this.Segment;
        }

        /// <summary>
        /// Определяет равен ли заданный объект <see cref="T:System.Object"/> текущему объекту
        /// </summary>
        /// <param name="obj">объект <see cref="T:System.Object"/></param>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            ScAddress scAddress = obj as ScAddress;
            if (scAddress as ScAddress == null)
                return false;
            return scAddress.Offset == this.Offset && scAddress.Segment== this.Segment;
        }

        /// <summary>
        /// Возвращает Hash код текущего объекта
        /// </summary>
        public override int GetHashCode()
        {  
            return Convert.ToInt32(this.Segment.ToString() + this.Offset.ToString());
        }

        /// <summary>
        /// Оператор сравнения адресов
        /// </summary>
        /// <param name="scAddress1">Первый адрес</param>
        /// <param name="scAddress2">Второй адрес</param>
        /// <returns>Возвращает True, если адреса равны</returns>
        public static bool operator ==(ScAddress scAddress1, ScAddress scAddress2)
        {
            bool isEqual = false;
            if (((object)scAddress1 != null) && ((object)scAddress2 != null))
            {
                isEqual = scAddress1.Equals(scAddress2);
            }

            return isEqual;
        }

        /// <summary>
        /// Оператор сравнения адресов
        /// </summary>
        /// <param name="scAddress1">Первый адрес</param>
        /// <param name="scAddress2">Второй адрес</param>
        /// <returns>Возвращает True, если адреса равны</returns>
        public static bool operator !=(ScAddress scAddress1, ScAddress scAddress2)
        {
            return !(scAddress1 == scAddress2);
        }

        #endregion 

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new byte[4];
            Array.Copy(BitConverter.GetBytes(segment), bytes, 2);
            Array.Copy(BitConverter.GetBytes(offset), 0, bytes, 2, 2);
            return bytes;
        }

        #endregion
    }
}
