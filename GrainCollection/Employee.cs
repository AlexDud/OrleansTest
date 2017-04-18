namespace GrainCollection
{
    using System;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;

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

        public async Task Greeting(IEmployee from, string message)
        {
            var name = await from.GetName();
            Console.WriteLine($"Employee {name} with Id {from.GetPrimaryKey()} said: {message}");
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