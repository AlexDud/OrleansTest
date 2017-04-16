namespace GrainCollection
{
    using GrainInterfaces;
    using System.Threading.Tasks;

    public class HelloGrain : Orleans.Grain, IHello
    {
        private string text = "Hello World!";

        public Task<string> SayHello(string greeting)
        {
            var oldText = text;
            text = greeting;
            return Task.FromResult(oldText);
        }
    }
}
