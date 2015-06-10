using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Ostis.Sctp.CallBacks;

namespace Ostis.Sctp.SyncClient
{
    internal class SynchronousClient : IClient, IDisposable
#warning Сделать этот класс IDisposable
    {
        private readonly Socket client;
        private readonly ReceiveEventArgs receiveArguments;

        private void OnReceive()
        {
            var handler = Volatile.Read(ref Received);
            if (handler != null)
            {
                handler(this, receiveArguments);
            }
        }

        public event ReceiveEventHandler Received;

        public SynchronousClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            receiveArguments = new ReceiveEventArgs();
        }

        public void Connect(string address, int port)
        {
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(address), port));
#warning Убрать все консольные вызовы из этого класса, так как в оконном приложении это вызовет ошибку.
                if (client.Connected)
                {
                    Console.WriteLine("Socket connected to " + client.RemoteEndPoint);
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.ToString());
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            disconnect();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// dtor.
        /// </summary>
        ~SynchronousClient()
        {
            disconnect();
        }

        bool disposed;

        private void disconnect()
        {
            if (!disposed)
            {
                client.Disconnect(true);
                //client.Close();
                client.Dispose();
                disposed = true;
            }
        }

        #endregion

        public void Send(byte[] bytes)
        {
            client.Send(bytes, bytes.Length, 0);
            Console.WriteLine("Sent {0} bytes to server.", bytes.Length);
            var bytesReceived = new byte[SctpProtocol.DefaultBufferSize];
            using (var stream = new MemoryStream())
            {
                int receivedSize = client.Receive(bytesReceived, 0, bytesReceived.Length, SocketFlags.None);
                stream.Write(bytesReceived, 0, receivedSize);
                while (client.Available != 0)
                {
                    receivedSize = client.Receive(bytesReceived, 0, bytesReceived.Length, SocketFlags.None);
                    stream.Write(bytesReceived, 0, receivedSize);
                    Thread.Sleep(0);
                }
                receiveArguments.ReceivedBytes = stream.ToArray();
            }
            OnReceive();
        }

        public bool Connected
        { get { return client.Connected; } }
    }
}
