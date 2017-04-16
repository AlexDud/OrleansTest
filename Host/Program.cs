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

            var hello = GrainClient.GrainFactory.GetGrain<IHello>(0);
            Console.WriteLine(hello.SayHello("First").Result);
            Console.WriteLine(hello.SayHello("Second").Result);
            Console.WriteLine(hello.SayHello("Third").Result);
            Console.WriteLine(hello.SayHello("Fourth").Result);
        }
    }
}

