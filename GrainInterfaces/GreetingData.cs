namespace GrainInterfaces
{
    using System;
    using Orleans.Concurrency;

    [Immutable]
    public class GreetingData
    {
        public Guid From { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
