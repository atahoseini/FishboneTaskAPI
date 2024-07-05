using Fishbone.Core;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.IRepositories;
using Fishbone.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fishbone.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderDbContext orderDbContext;
        private readonly EncryptionUtility encryptionUtility;

        public UserRepository(OrderDbContext orderDbContext, EncryptionUtility encryptionUtility)
        {
            this.orderDbContext = orderDbContext;
            this.encryptionUtility = encryptionUtility;
        }

        public async Task<User> GetAsync(string UserName)
        {
            if (UserName != null)
            {
                var query = orderDbContext.Users.AsQueryable();
                if (!string.IsNullOrEmpty(UserName))
                {
                    query = query.Where(u => u.UserName == UserName);
                }
                var foundUser = await query.FirstOrDefaultAsync();
                return foundUser;
            }
            else
            {
                throw new ArgumentNullException(UserName, "There is no user with this info");
            }
        }
        public async Task<User> GetAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "There is no user with this info");
            }
            Expression<Func<User, bool>> filter = u =>
                (user.Id == 0 || u.Id == user.Id) ||
                (string.IsNullOrEmpty(user.UserName) || u.UserName.Contains(user.UserName)) ||
                (string.IsNullOrEmpty(user.FirstName) || u.FirstName.Contains(user.FirstName)) ||
                (string.IsNullOrEmpty(user.LastName) || u.LastName.Contains(user.LastName));
            return await orderDbContext.Users.FirstOrDefaultAsync(filter);
        }
        public async Task<List<User>> GetAllAsync(int page, int size)
        {
            var foundUser = await orderDbContext.Users
                .Skip((page - 1) * size).Take(size)
                .AsNoTracking()
                .ToListAsync();
            return foundUser;
        }
        public async Task<List<User>> GetAllForLoadingAsync()
        {
            var foundUser = await orderDbContext.Users.AsNoTracking().ToListAsync();
            return foundUser;
        }
        public async Task<Int64> InsertAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var salt = encryptionUtility.GetNewSalt();
            var hashPassowrd = encryptionUtility.GetSHA256(user.Password.Trim(), salt);
            var newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = hashPassowrd,
                PasswordSalt = salt,
                RegisterDate = DateTime.UtcNow,
                LastLoginDate = null
            };
            await orderDbContext.Users.AddAsync(newUser);
            return newUser.Id;
        }

        public async Task<User> UpdateAsync(UserUpdateDto user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }
                var query = orderDbContext.Users.AsQueryable();
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    query = query.Where(u => u.UserName == user.UserName && u.Id==user.Id);
                }
                var foundUser = await query.FirstOrDefaultAsync();

                if (foundUser != null)
                {
                    foundUser.FirstName = user.FirstName;
                    foundUser.LastName = user.LastName;
                    //if (!string.IsNullOrWhiteSpace(user.Password))
                    //{
                    //    var salt = encryptionUtility.GetNewSalt();
                    //    var hashPassword = encryptionUtility.GetSHA256(user.Password.Trim(), salt);
                    //    foundUser.Password = hashPassword;
                    //    foundUser.PasswordSalt = salt;
                    //}
                    return foundUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(user), ex.Message);
            }
        }

        public async Task<Int64> RemoveAsync(User user)
        {
            if (user != null)
            {
                if (!orderDbContext.Users.Local.Contains(user))
                {
                    orderDbContext.Users.Attach(user);
                }
                orderDbContext.Users.Remove(user);
                return user.Id;
            }
            else
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
        }
        public async Task<int> CountAsync()
        {
            var count = await orderDbContext.Users.CountAsync();
            return count;
        }

    }
}
