using System;

namespace Ostis.Sctp.Arguments
{
	/// <summary>
	/// Событие в sc памяти
	/// </summary>
	public struct ScEvent:IArgument
	{


		private SubScriptionId _id;
		private ScAddress _elementaddress;
		private ScAddress _arcaddress;
		private byte[] _bytestream;

		/// <summary>
		/// Gets the ID
		/// </summary>
		/// <value>The I.</value>
		public SubScriptionId ID {
			get {
				return _id;
			}
		}

		/// <summary>
		/// Gets the element address.
		/// </summary>
		/// <value>The element address.</value>
		public ScAddress ElementAddress {
			get {
				return _elementaddress;
			}
		}

		/// <summary>
		/// Gets the arc address.
		/// </summary>
		/// <value>The arc address.</value>
		public ScAddress ArcAddress {
			get {
				return _arcaddress;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="sctp_client.Arguments.ScEvent"/> struct.
		/// </summary>
		/// <param name="ID">ID</param>
		/// <param name="ElementAddress">Element address.</param>
		/// <param name="ArcAddress">Arc address.</param>
		public ScEvent(SubScriptionId ID, ScAddress ElementAddress, ScAddress ArcAddress)
		{
			_id = ID;
			_elementaddress = ElementAddress;
			_arcaddress = ArcAddress;
			_bytestream = new byte[12];
		}

		#region IArgument implementation
		/// <summary>
		/// Возвращает длину массива байт аргумента
		/// </summary>
		/// <value>The length.</value>
		public uint Length {
			get {
				return (uint)_bytestream.Length;
			}
		}
		/// <summary>
		/// Возвращает массив байт аргумента
		/// </summary>
		/// <value>The bytes stream.</value>
		public byte[] BytesStream {
			get
			{
				_bytestream = new byte[12];
				Array.Copy(_id.BytesStream, _bytestream, 4);
				Array.Copy(_elementaddress.BytesStream, 0, _bytestream, 4, 4);
				Array.Copy(_arcaddress.BytesStream, 0, _bytestream, 8, 4);
				return _bytestream;
			}
		}

		#endregion

		/// <summary>
		/// Получает значение события из массива байт
		/// </summary>
		/// <param name="bytesstream">Массив байт </param>
		/// <param name="offset">Смещение в массиве</param>
		/// <returns></returns>
		public static ScEvent GetFromBytes(byte[] bytesstream, int offset)
		{
			ScEvent tmpevent= new ScEvent(new SubScriptionId (),new ScAddress (),new ScAddress ());
			if (bytesstream.Length >= sizeof(Int32) * 3 + offset)
			{
				SubScriptionId id = new SubScriptionId(BitConverter.ToInt32(bytesstream, sizeof(UInt32) * 0 + offset));
				ScAddress elementadr= ScAddress.GetFromBytes(bytesstream, sizeof(UInt32) * 1 + offset);
				ScAddress arcadr= ScAddress.GetFromBytes(bytesstream, sizeof(UInt32) * 2 + offset);
				tmpevent= new ScEvent(id,elementadr,arcadr);
			}

			return tmpevent;
		}




	}
}

