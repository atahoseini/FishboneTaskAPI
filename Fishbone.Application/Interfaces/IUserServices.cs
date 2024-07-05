using Fishbone.Core.Dto;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.Model;

namespace Fishbone.Application.Interfaces
{
    public interface IUserServices
    {
        Task<UserDto> Get(string userName);
        Task<TaskActionResult<List<UserDto>>> GetAll(int page, int size);
        Task<List<UserDto>> GetAllForLoading();
        Task<Int64> Add(User model);
        Task<UserDto> Update(UserUpdateDto model);
        Task<Int64> Delete(string userName);
    }
}
