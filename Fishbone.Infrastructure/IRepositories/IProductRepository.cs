using Fishbone.Core.Entities;

namespace Fishbone.Infrastructure.IRepositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Int64> InsertAsync(Product subject);
    }
}
