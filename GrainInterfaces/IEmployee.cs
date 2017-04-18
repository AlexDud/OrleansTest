namespace GrainInterfaces
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IEmployee : IGrainWithGuidKey
    {
        Task<int> GetLevel();
        Task Promote(int newLevel);
        Task Greeting(IEmployee from, string message);
        Task<IManager> GetManager();
        Task SetManager(IManager manager);
    }
}
