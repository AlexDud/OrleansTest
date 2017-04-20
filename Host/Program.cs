namespace Host
{
    using System;
    using GrainInterfaces;
    using Orleans;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            Console.ReadLine();

            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);

            // retrieve the MSFT stock
            var grain = GrainClient.GrainFactory.GetGrain<IStockGrain>("MSFT");
            var price = grain.GetPrice().Result;
            Console.WriteLine(price);

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();
        }
    }
}

