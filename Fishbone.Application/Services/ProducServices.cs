using AutoMapper;
using Fishbone.Application.Interfaces;
using Fishbone.Core;
using Fishbone.Core.Dto;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.Interfaces;
using Fishbone.Infrastructure.IRepositories;
using Fishbone.Infrastructure.Utility;
using Microsoft.Extensions.Logging;

namespace Fishbone.Application.Services
{
    public class ProducServices : IProductServices
    {
        private readonly OrderDbContext orderDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProducServices> logger;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProducServices(OrderDbContext orderDbContext, IMapper mapper, ILogger<ProducServices> logger,
                                IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.orderDbContext = orderDbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Add(Product product)
        {
            logger.LogInformation("Call Add from SubjectServices");
            try
            {
                await productRepository.InsertAsync(product);
                await unitOfWork.SaveChangesAsync();
                var result = mapper.Map<ProductDto>(product);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while adding a subject.", ex);
            }
        }

        public async Task<ProductDto> Get(int id)
        {
            logger.LogInformation("Call Get from SubjectServices");
            try
            {
                var taskItem = await productRepository.GetAsync(id);
                var result = mapper.Map<ProductDto>(taskItem);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while getting a subject.", ex);
            }
        }

        public async Task<List<ProductDto>> GetAll()
        {
            logger.LogInformation("Call GetAll from SubjectServices");
            try
            {
                var foundSubjects = await productRepository.GetAllAsync();
                var subjectDtos = mapper.Map<List<ProductDto>>(foundSubjects);
                logger.LogInformation("GetAll from SubjectService success call");
                return subjectDtos;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw new MyException("An error occurred while getting subjects.", ex);
            }
        }
    }
}
