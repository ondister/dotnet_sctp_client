using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ostis.Sctp.CallBacks;


namespace Ostis.Sctp.AsyncClient
{

	/// <summary>
	/// Этот класс всего лишь заготовка для асинхронного клиента, когда будет асинхронный сервер
	/// </summary>
   internal class ASctp_client:IClient
    {
        private  ManualResetEvent connectDone = new ManualResetEvent(false);
        private  ManualResetEvent disconnectDone = new ManualResetEvent(false);
        private  ManualResetEvent sendDone = new ManualResetEvent(false);
        private  ManualResetEvent receiveDone = new ManualResetEvent(false);

        public event ReceiveEventHandler Received;
        private ReceiveEventArgs arg;
        private void OnReceive() { if (Received != null) { Received(this, arg); } }


        private Socket _client;
        private StateObject _state;

        public ASctp_client()
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

                _client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _client);
                connectDone.WaitOne();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {

                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                connectDone.Set();
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
                _client.BeginDisconnect(true, new AsyncCallback(DisConnectCallback), _client);
                disconnectDone.WaitOne();
                _client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void DisConnectCallback(IAsyncResult ar)
        {
            try
            {

                Socket client = (Socket)ar.AsyncState;

                client.EndDisconnect(ar);

                Console.WriteLine("Socket disconnected from {0}", client.RemoteEndPoint.ToString());

                disconnectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public  void Send(byte[] bytestosend)
        {
            this.Send(bytestosend);
            sendDone.WaitOne();
            this.Receive(_client);
            receiveDone.WaitOne();
           
        }

        private  void SendAsync(byte[] bytes)
        {
            _client.BeginSend(bytes, 0, bytes.Length, 0,new AsyncCallback(SendCallback), _client);
        }

        private  void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        private  void  Receive(Socket client)
        {
            try
            {
                
               _state = new StateObject();
                _state.WorkSocket = client;

              
                _state.WorkSocket.BeginReceive(_state.Buffer, 0, StateObject.BufferSize, SocketFlags.None,
                                        new AsyncCallback(ReceiveCallback), _state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;

                int bytesRead = state.WorkSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                   state.Stream.Write(state.Buffer, 0, bytesRead);
                }

                if (bytesRead == StateObject.BufferSize)
                {
                    state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback), state);
                }

                else
                {
                    state.Stream.Close();
                    arg.ReceivedBytes = state.Stream.ToArray();
                    receiveDone.Set();
                    OnReceive();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public bool Connected
        {
            get {return _client.Connected; }
        }
    }

}
