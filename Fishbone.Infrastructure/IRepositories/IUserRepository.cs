using Fishbone.Core.Entities;

namespace Fishbone.Infrastructure.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userName);
        Task<User> GetAsync(User user);
        Task<List<User>> GetAllAsync(int page, int size);
        Task<List<User>> GetAllForLoadingAsync();

        Task<Int64> InsertAsync(User user);
        Task<User> UpdateAsync(UserUpdateDto user);
        Task<Int64> RemoveAsync(User user);
        Task<int> CountAsync();

    }
}
