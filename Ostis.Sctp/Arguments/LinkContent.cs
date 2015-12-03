using System;
using System.Collections.Generic;
using System.Drawing;
namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Содержимое SC-ссылки (пока поддерживаются только строки).
    /// </summary>
    ///   /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="LinkContent"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="LinkContent" lang="C#" />
    /// </example>
    public class LinkContent : IArgument,IEquatable<LinkContent>
    {
        /// <summary>
        /// Возвращает пустой контент
        /// </summary>
        public static readonly LinkContent Invalid = new LinkContent(String.Empty);

        /// <summary>
        /// OSTIS-тип содержимого ссылки.
        /// Принимает значения <see cref="T:Ostis.Sctp.LinkContentType"/>
        /// </summary>
        public LinkContentType ContentType
        { get { return contentType; } }

        /// <summary>
        /// Содержимое.
        /// </summary>
        public byte[] Data
        { get { return data; } }

        private readonly LinkContentType contentType;
        private readonly byte[] data;

        #region Конструкторы

        private LinkContent(LinkContentType contentType, byte[] data)
        {
            this.contentType = contentType;
            this.data = data;
        }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из массива байт.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public LinkContent(byte[] bytes)
            : this(LinkContentType.Unknown, bytes)
        { }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из строки.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(string value)
            : this(LinkContentType.Text, SctpProtocol.TextEncoding.GetBytes(value))
        { }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из типа Double.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(double value)
            : this(LinkContentType.Numeric, BitConverter.GetBytes(value))
		{ }

        /// <summary>
        /// Инициализирует новое содержимое ссылки из типа Int.
        /// </summary>
        /// <param name="value">значение</param>
        public LinkContent(int value)
            : this(LinkContentType.Numeric, BitConverter.GetBytes(value))
        { }

        #endregion

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
#warning закоментированные строки заменены добавлением аргумента с числом байт ссылки в команде setlinccontent
            //var bytes = new List<byte>();
            //bytes.AddRange(BitConverter.GetBytes(data.Length));
            //bytes.AddRange(data);
            //return bytes.ToArray();
            return data;
        }

        #endregion

        #region Статические члены
        /// <summary>
        /// Возвращает <see cref="System.String" /> из массива байт
        /// </summary>
        /// <param name="data">Массив байт</param>
        /// <returns>
        /// A <see cref="System.String" /> Строка содержимого ссылки
        /// </returns>
        public static string ToString(byte[] data)
        {
            return SctpProtocol.TextEncoding.GetString(data);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static int ToInt32(byte[] data)
        {
            int result;
            if (data.Length == 4)
            {
                result = BitConverter.ToInt32(data, 0);
            }
            else
            {
                string stringData = LinkContent.ToString(data);
                result = Int32.Parse(stringData);
            }
            return result;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static double ToDouble(byte[] data)
        {
            double result = double.NaN;
            if (data.Length == 8)
            {
                result= BitConverter.ToDouble(data,0);
            }
            else
            {
                string stringData = LinkContent.ToString(data);
                result= Double.Parse(stringData);
            }
            return result;
        }

        #endregion

        public bool Equals(LinkContent other)
        {
            return this.data == other.data & this.contentType==other.contentType;
        }
    }
}
