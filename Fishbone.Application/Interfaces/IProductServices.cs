using Fishbone.Core.Dto;
using Fishbone.Core.Entities;

namespace Fishbone.Application.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> Get(int id);
        Task<ProductDto> Add(Product model);
    }
}
