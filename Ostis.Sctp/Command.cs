using System.Collections.Generic;
using System.IO;
using System.Text;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    /// <summary>
    /// Абстрактный класс комманды
    /// </summary>
    public abstract class Command
    {
        internal CommandHeader Header
        { get; private set; }

        private Response response;

        /// <summary>
        /// Событие происходит при выполнении команды
        /// </summary>
        public event CommandDoneEventHandler CommandDone;

        private CommandDoneEventArgs commandDoneArgument;

        private void raiseCommandDone()
        {
#warning var handler = Volatile.Read(ref CommandDone);
            var handler = CommandDone;
            if (handler != null)
            {
                handler(this, commandDoneArgument);
            }
        }

        /// <summary>
        /// Аргументы команды
        /// </summary>
        public List<IArgument> Arguments
        { get; private set; }
        
		public uint Code
		{ get { return Header.Code; } }

		/// <summary>
        ///Возвращает и задает уникальный идентификатор команды
        /// </summary>
        /// <value>
        /// Уникальный идентификатор команды
        /// </value>
        public uint Id
        {
            get { return Header.Id; }
            set { Header.Id = value; }
        }

        /// <summary>
        /// Конструктор класса <see cref="Command"/> 
        /// </summary>
        /// <param name="code">Код команды</param>
        /// <param name="flags">Флаг команды</param>
        protected Command(byte code, byte flags)
        {
            Header = new CommandHeader
            {
                Code = code,
                Flags = flags,
            };
            Arguments = new List<IArgument>();
            commandDoneArgument = new CommandDoneEventArgs();
        }

        /// <summary>
        /// Возвращает массив байт команды
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
        /// Возвращает и задает ответ сервера команды
        /// </summary>
        public Response Response
        {
            get { return response; }
            set
            {
                response = value;
                commandDoneArgument.ReturnCode = response.Header.ReturnCode;
                raiseCommandDone();
            }
        }
    }
}
