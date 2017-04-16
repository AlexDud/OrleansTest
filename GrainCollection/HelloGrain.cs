namespace GrainCollection
{
    using GrainInterfaces;
    using System.Threading.Tasks;

    class HelloGrain : Orleans.Grain, IHello
    {
        public Task<string> SayHello(string msg)
        {
            return Task.FromResult($"You said {msg}, I say: Hello!");
        }
    }
}
