using AutoMapper;
using Fishbone.Application.Interfaces;
using Fishbone.Core;
using Fishbone.Core.Dto;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.Interfaces;
using Fishbone.Infrastructure.IRepositories;
using Fishbone.Infrastructure.Model;
using Fishbone.Infrastructure.Utility;
using Microsoft.Extensions.Logging;

namespace Fishbone.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly OrderDbContext orderDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<UserServices> logger;
        private readonly EncryptionUtility encryptionUtility;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserServices(OrderDbContext orderDbContext, IMapper mapper, ILogger<UserServices> logger,
                                EncryptionUtility encryptionUtility, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.orderDbContext = orderDbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.encryptionUtility = encryptionUtility;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Get(string userName)
        {
            logger.LogInformation("Call Get from UserServices");
            try
            {
                var user = await userRepository.GetAsync(userName);
                var result = mapper.Map<UserDto>(user);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while getting a user.", ex);
            }
        }

        public async Task<TaskActionResult<List<UserDto>>> GetAll(int page, int size)
        {
            logger.LogInformation("Call GetAll from UserService");
            var result = new TaskActionResult<List<UserDto>>();
            try
            {
                var foundUser = await userRepository.GetAllAsync(page, size);
                var users = mapper.Map<List<UserDto>>(foundUser);
                var totalRecordCount = await userRepository.CountAsync();
                result.IsSuccess = true;
                result.Data = users;
                result.Page = page;
                result.Size = size;
                result.Total = totalRecordCount;
                logger.LogInformation("GetAll from UserService success call");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<List<UserDto>> GetAllForLoading()
        {
            logger.LogInformation("Call GetAllForLoading from UserService");
            var result = new List<UserDto>();
            try
            {
                var foundUser = await userRepository.GetAllForLoadingAsync();
                result = mapper.Map<List<UserDto>>(foundUser);
                logger.LogInformation("GetAll from UserService success call");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while getting users for loading.", ex);
            }
            return result;
        }
        public async Task<long> Add(User user)
        {
            logger.LogInformation("Call Add from UserServices");
            try
            {
                var id = await userRepository.InsertAsync(user);
                await unitOfWork.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while adding a user.", ex);
            }
        }
        public async Task<UserDto> Update(UserUpdateDto model)
        {
            logger.LogInformation("Call Update from UserServices");
            try
            {
                var user = await userRepository.UpdateAsync(model);
                await unitOfWork.SaveChangesAsync();
                var tempUserDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                return tempUserDto;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while updating a user.", ex);
            }
        }

        public async Task<long> Delete(string userName)
        {
            logger.LogInformation("Call Delete from UserServices");
            try
            {
                var user = await userRepository.GetAsync(userName);
                if (user != null)
                {
                    await userRepository.RemoveAsync(user);
                    await unitOfWork.SaveChangesAsync();
                    var result = mapper.Map<UserDto>(user);
                    return result.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while deleting a user.", ex);
            }
        }
    }

}
