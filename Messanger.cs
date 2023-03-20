using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using MyShop.CommonLib;

namespace Client
{
    internal class Messanger
    {
        private static async Task BuildAndSendCommandAsync(Socket socket, Boolean logged)
        {
            String jsonCommand = null;
            if (!logged)
            {
                jsonCommand = ActionsProvider.ChooseAvailableActionsBeforeLogin();
            }
            else
            {
                ActionsProvider.ChooseAvailableActionsAfterLogin();
            }
            await MessageProcessor.SendMessageAsync(jsonCommand, socket);
        }

        private static async Task ReceiveResponseAsync(Socket socket)//to separate class when ResponseInfo in CommonLib is implemented
        {
            throw new NotImplementedException();
        }

        public static async Task SendAndReceiveCycle(Socket serverSocket, Boolean logged)
        {
            if (serverSocket == null)
                throw new ArgumentNullException(nameof(serverSocket));

            try
            {
                while(true) 
                {
                    await BuildAndSendCommandAsync(serverSocket, logged);
                    await ReceiveResponseAsync(serverSocket);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error within SendAndReceive cycle\nError: {0}", ex.Message);
                throw;
            }
        }
    }
}
