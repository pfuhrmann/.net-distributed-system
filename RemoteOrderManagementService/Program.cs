using System;
using System.Runtime.Remoting;

namespace RemoteOrderManagementService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Remote.config", false);
            Console.WriteLine("Listening for requests. Press Enter to exit...");
            Console.ReadLine();
        }
    }
}