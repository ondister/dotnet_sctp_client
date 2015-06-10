using System.Collections.Generic;
using System.IO;
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
        internal CommandHeader Header
        { get; private set; }

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
        /// Аргументы.
        /// </summary>
        public List<IArgument> Arguments
        { get; private set; }
        
        /// <summary>
        /// Код.
        /// </summary>
		public CommandCode Code
        { get { return (CommandCode) Header.Code; } }

		/// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public uint Id
        {
            get { return Header.Id; }
            set { Header.Id = value; }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="flags">флаги</param>
        protected Command(CommandCode code, byte flags)
        {
            Header = new CommandHeader
            {
                Code = (byte) code,
#warning Можно ли преобразовать этот байт во флаговый enum?
                Flags = flags,
            };
            Arguments = new List<IArgument>();
            commandDoneArgument = new CommandDoneEventArgs();
        }

#warning Переименовать в Bytes и рассмотреть возможность сохранения в виде поля или преобразовать в метод.
        /// <summary>
        /// Массив байт.
        /// </summary>
        public byte[] BytesStream
        {
            get
            {
                var stream = new MemoryStream();
                if (Header.Id != 0)
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                    {
                        writer.Write(Header.BytesStream);
                        foreach (var argument in Arguments)
                        {
                            writer.Write(argument.BytesStream);
                        }
                    }
                }
                return stream.ToArray();
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
    }
}
