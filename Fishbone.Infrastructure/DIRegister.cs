using Fishbone.Infrastructure.Interfaces;
using Fishbone.Infrastructure.IRepositories;
using Fishbone.Infrastructure.Repository;
using Fishbone.Infrastructure.UnitOfWorks;
using Fishbone.Infrastructure.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fishbone.Infrastructure
{
    public static class DIRegister
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IProductRepository, ProductReository>();

        }
        public static void AddUnitOfWork(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddInfraUtility(this IServiceCollection service)
        {
            service.AddSingleton<EncryptionUtility>();
        }
    }
}
