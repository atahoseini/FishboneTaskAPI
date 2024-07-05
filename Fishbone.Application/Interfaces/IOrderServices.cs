using Fishbone.Core.Dto;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.Model;

namespace Fishbone.Application.Interfaces
{
    public interface IOrderServices
    {
        Task<TaskActionResult<List<OrderDto>>> GetAll(int page, int size);
        Task<OrderDto> Get(int id);
        Task<Order> Add(OrderModifiedDto model);
        Task<Order> Update(OrderModifiedDto model);
        Task<int> Delete(int id);
    }
}
