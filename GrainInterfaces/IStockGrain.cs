namespace GrainInterfaces
{
    using System.Threading.Tasks;

    public interface IStockGrain : Orleans.IGrainWithStringKey
    {
        Task<string> GetPrice();
    }
}
