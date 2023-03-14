using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Client : IDisposable
    {
        private readonly Socket _socket;

        private const String SERVER_URL = "127.0.0.1";

        private const Int32 SERVER_PORT = 6667;

        private const Int32 BUFFER_SIZE = 512;

        public Client()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public Boolean ConnectToServer()
        {
            try
            {
                _socket.Connect(SERVER_URL, SERVER_PORT);
                if (_socket.Connected)
                {
                    Console.WriteLine("Successfully connected");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return _socket.Connected;
        }

        private void ReceiveMessage()
        {
            try
            {
                Byte[] buffer = new Byte[BUFFER_SIZE];
                StringBuilder sb = new StringBuilder();
                do
                {
                    Int32 bytes = _socket.Receive(buffer, BUFFER_SIZE, 0);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (_socket.Available != 0);

                Console.WriteLine("[{0}]\t{1}", DateTime.Now, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to receive message from server: {0}", ex.Message);
                throw;
            }
            
        }

        private void SendMessage(String message)
        {
            try
            {
                Byte[] buffer = Encoding.UTF8.GetBytes(message);
                _socket.Send(buffer, buffer.Length, SocketFlags.None);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Failed to send message to server: {0}", ex.Message);
                throw;
            }
        }

        public void SendReceiveCycle()
        {
            try
            {
                while (true)
                {
                    SendMessage(Console.ReadLine());
                    ReceiveMessage();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is a fail within SendReceiveCycle: {0}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}
