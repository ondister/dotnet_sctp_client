using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using sctp_client.CallBacks;
using System.Threading;

namespace sctp_client.SyncClient
{
   internal class SSctp_Client:IClient
    {
       private Socket _client;
       private ReceiveEventArgs arg;
       private void OnReceive() { if (Received != null) { Received(this, arg); } }


        public event CallBacks.ReceiveEventHandler Received;

        public SSctp_Client()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            arg = new ReceiveEventArgs();
        }


        public void Connect(string address, int port)
        {
            try
            {

                IPHostEntry ipHostInfo = Dns.GetHostEntry(address);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                _client.Connect(remoteEP);
                if (_client.Connected)
                {
                    Console.WriteLine("Socket connected to {0}", _client.RemoteEndPoint.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_client.Connected)
                {
                    _client.Disconnect(true);
                    _client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendBytes(byte[] bytestosend)
        { 
				_client.Send (bytestosend, bytestosend.Length, 0);
				Console.WriteLine ("Sent {0} bytes to server.", bytestosend.Length);
            
            
				Byte[] bytesReceived = new Byte[1024];
				MemoryStream stream = new MemoryStream ();
            
				int bytes = _client.Receive (bytesReceived, 0, bytesReceived.Length, SocketFlags.None);
				stream.Write (bytesReceived, 0, bytes);

				while (_client.Available != 0) {
					bytes = _client.Receive (bytesReceived, 0, bytesReceived.Length, SocketFlags.None);
					stream.Write (bytesReceived, 0, bytes);
					Thread.Sleep (0);
				}
           
				stream.Close ();
				arg.ReceivedBytes = stream.ToArray ();
				OnReceive ();


        }

        public bool Connected
        {
            get { return _client.Connected; }
        }
    }
}
