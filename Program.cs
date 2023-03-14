using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            Client client = null;
            try
            {
                client = new Client();
                client.ConnectToServer();
                client.SendReceiveCycle();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
            finally
            {
                client?.Dispose();
            }
        }
    }
}
