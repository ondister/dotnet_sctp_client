using System;
using System.Collections.Generic;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Идентификатор SC-элемента.
    /// </summary>
    /// <example>
    /// Следующий пример демонстрирует использование класса: <see cref="Identifier"/>
    /// <code source="..\Ostis.Tests\ArgumentsTest.cs" region="Identifier" lang="C#" />
    /// </example>
    public class Identifier : IArgument,IEquatable<Identifier>
    {
        /// <summary>
        /// Значение.
        /// </summary>
        public string Value
        { get { return value; } }

        private readonly string value;

        /// <summary>
        /// Инициализирует новый идентификатор SC-элемента.
        /// </summary>
        /// <param name="value">значение</param>
        public Identifier(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Возвращает пустой идентификатор, который не разрешен в системе
        /// </summary>
        public static readonly Identifier Invalid = String.Empty;

        /// <summary>
        /// Возвращает метку уникального идентификатора
        /// </summary>
        public static readonly Identifier Unique = "identifierUnique";


        /// <summary>
        /// Преобразование Идентификатора из строки.
        /// </summary>
        /// <param name="value">строковое значение</param>
        /// <returns>SC-идентификатор</returns>
        public static implicit operator Identifier(string value)
        {
            return new Identifier(value);
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
            return value;
        }

        /// <summary>
        /// Определяет равен ли заданный объект <see cref="Identifier"/> текущему объекту
        /// </summary>
        /// <param name="obj">объект <see cref="Identifier"/></param>
        public bool Equals(Identifier obj)
        {
            if (obj == null)
                return false;

            return obj.Value == this.Value;
        }

        /// <summary>
        /// Определяет равен ли заданный объект <see cref="T:System.Object"/> текущему объекту
        /// </summary>
        /// <param name="obj">объект <see cref="T:System.Object"/></param>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Identifier identifier = obj as Identifier;
            if (identifier as Identifier == null)
                return false;
            return identifier.Value == this.Value;
        }

        /// <summary>
        /// Возвращает хэш-код значения
        /// </summary>
        /// <returns>Хэш-код значения</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

     /// <summary>
     /// Оператор сравнения идентификаторов
     /// </summary>
     /// <param name="identifier1">Первый идентификатор</param>
     /// <param name="identifier2">Второй идентификатор</param>
     /// <returns>Возвращает True, если значения идентификаторов равны</returns>
        public static bool operator ==(Identifier identifier1,Identifier identifier2)
        {
          bool  isEqual = false;
          if (((object)identifier1 != null) && ((object)identifier2 != null))
          {
              isEqual = identifier1.Equals(identifier2);
          }

            return isEqual;
        }

        /// <summary>
        /// Оператор сравнения идентификаторов
        /// </summary>
        /// <param name="identifier1">Первый идентификатор</param>
        /// <param name="identifier2">Второй идентификатор</param>
        /// <returns>Возвращает True, если значения идентификаторов не равны</returns>
        public static bool operator !=(Identifier identifier1, Identifier identifier2)
        {
            return !(identifier1 == identifier2);
        }

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytesWithoutLength = SctpProtocol.TextEncoding.GetBytes(value);
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(bytesWithoutLength.Length));
            bytes.AddRange(bytesWithoutLength);
            return bytes.ToArray();
        }

        #endregion
    }
}
