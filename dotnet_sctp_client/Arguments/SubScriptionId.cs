using System;

namespace sctp_client.Arguments
{
	public struct SubScriptionId:IArgument
	{
		private Int32 _id;
		#region IArgument implementation
		private byte[] _bytestream;
		public uint Length {
			get { return (uint)_bytestream.Length; }
		}

		public byte[] BytesStream {
			get {
				Array.Copy(BitConverter.GetBytes(_id), _bytestream,4);
				return _bytestream;
			}
		}
		#endregion

		public Int32 ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public SubScriptionId(Int32 id)
		{
			_id = id;
			_bytestream = new byte[4];
		}

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

