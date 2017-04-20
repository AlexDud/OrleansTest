namespace GrainCollection
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;

    public class Manager : Grain, IManager
    {
        private IEmployee _me;
        private List<IEmployee> _reports = new List<IEmployee>();

        public override Task OnActivateAsync()
        {
            _me = GrainFactory.GetGrain<IEmployee>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }

        public Task<List<IEmployee>> GetDirectReports()
        {
            return Task.FromResult(_reports);
        }

        public async Task AddDirectReport(IEmployee employee)
        {
            _reports.Add(employee);
            await employee.SetManager(this);
            await employee.Greeting(new GreetingData
            {
                From = this.GetPrimaryKey(),
                Message = "Welcome to my team!"
            });
        }

        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }
    }
}