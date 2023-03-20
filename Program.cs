using System;
using System.Threading.Tasks;
using MyShop.CommonLib;

namespace Client
{
    internal class Program
    {
        static async Task Main()
        {
            Client client = null;
            try
            {
                using (client = new Client())
                {
                    await client.StartAsync();
                }
            }
            catch (Exception ex)
            {
                client?.Dispose();
                Console.WriteLine("Error in Main Cycle, closing the app with code 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
            
        }
    }
}
