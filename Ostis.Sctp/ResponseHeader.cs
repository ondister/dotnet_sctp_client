using System;
using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp
{
    /// <summary>
    /// Заголовок ответа сервера
    /// </summary>
    public class ResponseHeader
    {
        private byte _code;
        private UInt32 _id;
        private byte _returncode;
        private UInt32 _returnsize;

        private int _leight;

        /// <summary>
        /// Длина заголовка
        /// </summary>
        public int Leight
        {
            get { return _leight; }
           
        }

        /// <summary>
        /// Код команды, на которую получен ответ
        /// </summary>
        public byte Code
        {
            get { return _code; }
        }

        /// <summary>
        /// Уникальный идентификатор команды в потоке команд
        /// </summary>
       public UInt32 Id
        {
            get { return _id; }
        }

       /// <summary>
       ///Возвращает код успешности выполнения команды
       /// </summary>
        public enumReturnCode ReturnCode
       {
           get { return (enumReturnCode)_returncode; }
       }

        /// <summary>
        /// Возвращает размер содержимого ответа
        /// </summary>
        public UInt32 ReturnSize
        {
            get { return _returnsize; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResponseHeader"/>
        /// </summary>
        /// <param name="bytesdata">Массив байт заголовка</param>
        public ResponseHeader(byte[] bytesdata)
        {
            if (bytesdata.Length >= 10)
            {
                _code = bytesdata[0];
                _id = BitConverter.ToUInt32(bytesdata, 1);
                _returncode = bytesdata[5];
                _returnsize = BitConverter.ToUInt32(bytesdata, 6);
                _leight = bytesdata.Length;
            }
        }

    }
}
