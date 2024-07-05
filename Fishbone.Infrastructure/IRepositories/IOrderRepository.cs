using Fishbone.Core.Dto;
using Fishbone.Core.Entities;

namespace Fishbone.Infrastructure.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(int id);
        Task<List<Order>> GetAllAsync(int page, int size);
        Task<Int64> InsertAsync(OrderModifiedDto TaskItem);
        Task<Order> UpdateAsync(OrderModifiedDto TaskItem);
        Task<int> RemoveAsync(int id);
        Task<int> CountAsync();
    }
}
