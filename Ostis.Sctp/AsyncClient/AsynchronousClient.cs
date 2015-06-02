using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.AsyncClient
{
    // Этот класс всего лишь заготовка для асинхронного клиента, когда будет асинхронный сервер
#warning Асинхронному клиенту не нужен асинхронный сервер. Просто реализовать асинхронную работу.
//я делал, даже работало. Крайне нужно протестить на скорость после окончания работы, есть предположения, что работать буде медленней
    internal class AsynchronousClient : IClient
    {
        private readonly ManualResetEvent connectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent disconnectDone = new ManualResetEvent(false);
        private readonly ManualResetEvent sendDone = new ManualResetEvent(false);
        private readonly ManualResetEvent receiveDone = new ManualResetEvent(false);

        public event ReceiveEventHandler Received;
        private readonly ReceiveEventArgs receiveArgs;

	    private void raiseReceived()
	    {
	        var handler = Volatile.Read(ref Received);
            if (handler != null)
	        {
                handler(this, receiveArgs);
	        }
	    }

	    private readonly Socket client;
        private StateObject state;

        public AsynchronousClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            receiveArgs = new ReceiveEventArgs();
        }

        public void Connect(string address, int port)
        {
            try
            {
#warning Взятие адреса сделать аналогично синхронному клиенту.
#warning Передача клиента в метод необязательна, так как он и так доступен как поле.
                client.BeginConnect(new IPEndPoint(Dns.GetHostEntry(address).AddressList[0], port), connectCallback, client);
                connectDone.WaitOne();
            }
            catch (Exception exception)
            {
#warning Из этого класса нужно тоже удалить все вызовы консоли.
                Console.WriteLine(exception.ToString());
            }
        }

        private void connectCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = (Socket) asyncResult.AsyncState;
                client.EndConnect(asyncResult);
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);
                connectDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public void Disconnect()
        {
            try
            {
                client.BeginDisconnect(true, disconnectCallback, client);
                disconnectDone.WaitOne();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void disconnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = (Socket) asyncResult.AsyncState;
                client.EndDisconnect(asyncResult);
                Console.WriteLine("Socket disconnected from {0}", client.RemoteEndPoint);
                disconnectDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public void Send(byte[] bytestosend)
        {
            Send(bytestosend);
            sendDone.WaitOne();
            receive(client);
            receiveDone.WaitOne();
        }

        private void sendAsync(byte[] bytes)
        {
            client.BeginSend(bytes, 0, bytes.Length, 0, sendCallback, client);
        }

        private void sendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                sendDone.Set();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        
        private void receive(Socket client)
        {
            try
            {
                state = new StateObject { WorkSocket = client };
                state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, receiveCallback, state);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void receiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                var state = (StateObject) asyncResult.AsyncState;
                int bytesRead = state.WorkSocket.EndReceive(asyncResult);
                if (bytesRead > 0)
                {
                   state.Stream.Write(state.Buffer, 0, bytesRead);
                }

                if (bytesRead == StateObject.BufferSize)
                {
                    state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, receiveCallback, state);
                }
                else
                {
                    state.Stream.Close();
                    receiveArgs.ReceivedBytes = state.Stream.ToArray();
                    receiveDone.Set();
                    raiseReceived();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool Connected
        { get { return client.Connected; } }
    }
}
