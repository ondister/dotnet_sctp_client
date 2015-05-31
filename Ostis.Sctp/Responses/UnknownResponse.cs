namespace Ostis.Sctp.Responses
{
    /// <summary>
    /// Ответ на команду неизвестен.
    /// </summary>
    public class UnknownResponse : Response
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        public UnknownResponse(byte[] bytes)
            : base(bytes)
        { }
    }
}
