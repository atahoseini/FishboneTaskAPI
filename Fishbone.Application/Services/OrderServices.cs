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
    public class OrderServices : IOrderServices
    {
        private readonly OrderDbContext orderDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<OrderServices> logger;
        private readonly IOrderRepository orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderServices(OrderDbContext orderDbContext, IMapper mapper, ILogger<OrderServices> logger,
                                IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this.orderDbContext = orderDbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> Get(int id)
        {
            logger.LogInformation("Call Get from Order Services");
            try
            {
                var order = await orderRepository.GetAsync(id);
                var result = mapper.Map<OrderDto>(order);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while getting a order.", ex);
            }
        }

        public async Task<TaskActionResult<List<OrderDto>>> GetAll(int page, int size)
        {
            logger.LogInformation("Call GetAll from Order Service");
            var result = new TaskActionResult<List<OrderDto>>();
            try
            {
                var foundTask = await orderRepository.GetAllAsync(page, size);
                var orders = mapper.Map<List<OrderDto>>(foundTask);
                var totalRecordCount = await orderRepository.CountAsync();

                result.IsSuccess = true;
                result.Data = orders;
                result.Page = page;
                result.Size = size;
                result.Total = totalRecordCount;
                logger.LogInformation("GetAll from Order Service success call");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<Order> Add(OrderModifiedDto order)
        {
            logger.LogInformation("Call Add from Order Service");
            try
            {
                await orderRepository.InsertAsync(order);
                await unitOfWork.SaveChangesAsync();
                var result = mapper.Map<Order>(order);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while adding a order item.", ex);
            }
        }

        public async Task<Order> Update(OrderModifiedDto model)
        {
            logger.LogInformation("Call Update from Order Service");
            try
            {
                var tasks = await orderRepository.UpdateAsync(model);
                await unitOfWork.SaveChangesAsync();
                if (tasks != null)
                {
                    var result = mapper.Map<Order>(tasks);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while updating a task item.", ex);
            }
        }

        public async Task<int> Delete(int id)
        {
            logger.LogInformation("Call Delete from Order Service");
            try
            {
                var result = await orderRepository.RemoveAsync(id);
                await unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while deleting a task item.", ex);
            }
        }
    }

}
