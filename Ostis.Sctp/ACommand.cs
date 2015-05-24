using System.Collections.Generic;
using System.IO;
using System.Text;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    /// <summary>
    /// Абстрактный класс комманды
    /// </summary>
    public abstract class ACommand
    {
        internal CommandHeader _header;
        protected List<IArgument> _arguments;
        private AResponse _response;

        /// <summary>
        /// Событие происходит при выполнении команды
        /// </summary>
        public event CommandDoneEventHandler CommandDone;

        private CommandDoneEventArgs arg;

        private void OnCommandDone() { if (CommandDone != null) { CommandDone(this, arg); } }

        /// <summary>
        /// Аргументы команды
        /// </summary>
        public List<IArgument> Arguments
        {
            get { return _arguments; }
        }

        
		public uint Code
		{
			get
			{
				return _header.Code;
			}

		}

		/// <summary>
        ///Возвращает и задает уникальный идентификатор команды
        /// </summary>
        /// <value>
        /// Уникальный идентификатор команды
        /// </value>
        public uint Id
        {
            get
            {
                return _header.Id;
            }
            set
            {
                _header.Id = value;
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="ACommand"/> 
        /// </summary>
        /// <param name="code">Код команды</param>
        /// <param name="flag">Флаг команды</param>
        public ACommand(byte code,byte flag)
        {
            _header = new CommandHeader();
            _header.Code = code;
            _header.Flags = flag;
            _arguments = new List<IArgument>();
            arg = new CommandDoneEventArgs();
        }

        /// <summary>
        /// Возвращает массив байт команды
        /// </summary>
        public byte[] BytesStream
        {
            get
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);

                if (_header.Id != 0)
                {
                    writer.Write(_header.BytesStream);

                    foreach (IArgument arg in _arguments)
                    {
                        writer.Write(arg.BytesStream);
                    }

                }
                writer.Close();

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Возвращает и задает ответ сервера команды
        /// </summary>
  
        public AResponse Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                arg.ReturnCode = _response.Header.ReturnCode;
                this.OnCommandDone();
            }
        }
    }
}
