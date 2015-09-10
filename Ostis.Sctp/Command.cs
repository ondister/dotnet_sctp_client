using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ostis.Sctp
{
    /// <summary>
    /// Абстрактный класс команды
    /// </summary>
    public abstract class Command
    {
        #region Свойства

        /// <summary>
        /// Код.
        /// </summary>
        public CommandCode Code
        { get; set; }

        /// <summary>
        /// Байт параметров (флагов) команды.
        /// </summary>
        public byte Flags
        { get; set; }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public uint Id
        { get; internal set; }

        /// <summary>
        /// Аргументы.
        /// </summary>
        public List<IArgument> Arguments
        { get; private set; }

        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="code">код</param>
        protected Command(CommandCode code)
        {
            Code = code;
            Arguments = new List<IArgument>();
        }

        /// <summary>
        /// Получение массива байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var stream = new MemoryStream();
          if (Id != 0)
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write((byte) Code);
                    writer.Write(Flags);
                    writer.Write(Id);
                    var argumentsBytes = new List<byte[]>();
                    foreach (var argument in Arguments)
                    {
                        argumentsBytes.Add(argument.GetBytes());
                    }
                    writer.Write((uint) argumentsBytes.Sum(a => a.Length));
                    foreach (var argument in argumentsBytes)
                    {
                        writer.Write(argument);
                    }
                }
            }
            return stream.ToArray();
        }
    }
}
