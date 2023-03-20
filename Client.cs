using MyShop.CommonLib;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Client : IDisposable
    {
        private readonly Socket _serverSocket;

        private Boolean _logged;
        
        private const String SERVER_IP = "127.0.0.1";

        private const Int32 SERVER_PORT = 6667;


        public Client()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _logged = false;
        }

        public async Task StartAsync()
        {
            try
            {
                await ConnectToServerAsync(TimeSpan.FromSeconds(10));
                await Messanger.SendAndReceiveCycle(_serverSocket, _logged);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in Start method occured, message: {0}", ex.Message);
                throw;
            }
        }

        private async Task ConnectToServerAsync(TimeSpan timeToRetry)
        {
            while (true)
            {
                try
                {
                    await _serverSocket.ConnectAsync(SERVER_IP, SERVER_PORT);
                    if (_serverSocket.Connected)
                    {
                        Console.WriteLine("Successfully connected to server...");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to server\nError: {0}", ex.Message);
                    Console.WriteLine("Waiting {0} seconds before retry", timeToRetry.TotalSeconds);
                    Thread.Sleep(timeToRetry);
                }
            }
        }

        public void Dispose()
        {
            _serverSocket?.Dispose();
        }
    }
}
