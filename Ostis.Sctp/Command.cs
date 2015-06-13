﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    /// <summary>
    /// Команда.
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
        { get; set; }

        /// <summary>
        /// Аргументы.
        /// </summary>
        public List<IArgument> Arguments
        { get; private set; }

        #endregion

        #region Obsolete
#warning Obsolete

        private Response response;

#warning Необходимость этого события - под вопросом.
        /// <summary>
        /// Выполнение команды.
        /// </summary>
        public event CommandDoneEventHandler CommandDone;

        private CommandDoneEventArgs commandDoneArgument;

        private void raiseCommandDone()
        {
            var handler = Volatile.Read(ref CommandDone);
            if (handler != null)
            {
                handler(this, commandDoneArgument);
            }
        }

        /// <summary>
        /// Ответ сервера.
        /// </summary>
        public Response Response
        {
            get { return response; }
            internal set
            {
                response = value;
                commandDoneArgument.ReturnCode = response.Header.ReturnCode;
                raiseCommandDone();
            }
        }

        #endregion

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="code">код</param>
        protected Command(CommandCode code)
        {
            Code = code;
            Arguments = new List<IArgument>();
            commandDoneArgument = new CommandDoneEventArgs();
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
#warning Оптимизировать, чтобы не вызывать вычислимый a.BytesStream каждый раз!
                    writer.Write((uint) Arguments.Sum(a => a.GetBytes().Length));
                    foreach (var argument in Arguments)
                    {
                        writer.Write(argument.GetBytes());
                    }
                }
            }
            return stream.ToArray();
        }
    }
}
