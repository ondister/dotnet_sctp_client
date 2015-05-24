using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Sub scription identifier.
	/// </summary>
	public struct SubScriptionId:IArgument
	{
		private Int32 _id;
		#region IArgument implementation
		private byte[] _bytestream;

		/// <summary>
		/// Возвращает длину массива байт аргумента
		/// </summary>
		/// <value>The length.</value>
		public uint Length {
			get { return (uint)_bytestream.Length; }
		}
		/// <summary>
		/// Возвращает массив байт аргумента
		/// </summary>
		/// <value>The bytes stream.</value>
		public byte[] BytesStream {
			get {
				Array.Copy(BitConverter.GetBytes(_id), _bytestream,4);
				return _bytestream;
			}
		}
		#endregion
		/// <summary>
		/// Gets or sets the I.
		/// </summary>
		/// <value>The I.</value>
		public Int32 ID
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="sctp_client.Arguments.SubScriptionId"/> struct.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public SubScriptionId(Int32 id)
		{
			_id = id;
			_bytestream = new byte[4];
		}
		/// <summary>
		/// Gets from bytes.
		/// </summary>
		/// <returns>The from bytes.</returns>
		/// <param name="bytesstream">Bytesstream.</param>
		public static Int32 GetFromBytes(byte[] bytesstream)
		{
			Int32 tmpint = 0;
			if (bytesstream.Length >= sizeof(Int32))
			{

				tmpint= BitConverter.ToInt32(bytesstream, sizeof(Int32));
			}


			return tmpint;
		}


	}
}

