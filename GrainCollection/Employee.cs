namespace GrainCollection
{
    using System;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;
    using Orleans.Concurrency;

    [Reentrant]
    public class Employee : Grain, IEmployee
    {
        private int _level;
        private string _name;
        private IManager _manager;

        public Task<int> GetLevel()
        {
            return Task.FromResult(_level);
        }

        public Task Promote(int newLevel)
        {
            _level = newLevel;
            return TaskDone.Done;
        }

        public Task<string> GetName()
        {
            return Task.FromResult(_name);
        }

        public Task SetName(string newName)
        {
            _name = newName;
            return TaskDone.Done;
        }

        public async Task Greeting(GreetingData data)
        {
            Console.WriteLine("{0} said: {1}", data.From, data.Message);

            // stop this from repeating endlessly
            if (data.Count >= 3) return;

            // send a message back to the sender
            var fromGrain = GrainFactory.GetGrain<IEmployee>(data.From);
            await fromGrain.Greeting(new GreetingData
            {
                From = this.GetPrimaryKey(),
                Message = "Thanks!",
                Count = data.Count + 1
            });
        }

        public Task<IManager> GetManager()
        {
            return Task.FromResult(_manager);
        }

        public Task SetManager(IManager manager)
        {
            _manager = manager;
            return TaskDone.Done;
        }
    }
}