﻿namespace GrainCollection
{
    using System;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;

    public class Employee : Grain, IEmployee
    {
        private int _level;
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

        public Task Greeting(IEmployee from, string message)
        {
            Console.WriteLine("{0} said: {1}", from.GetPrimaryKey(), message);
            return TaskDone.Done;
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