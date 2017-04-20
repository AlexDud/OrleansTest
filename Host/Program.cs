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

            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);

            var e0 = GrainClient.GrainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var m1 = GrainClient.GrainFactory.GetGrain<IManager>(Guid.NewGuid());
            m1.AddDirectReport(e0).Wait();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();
        }
    }
}

