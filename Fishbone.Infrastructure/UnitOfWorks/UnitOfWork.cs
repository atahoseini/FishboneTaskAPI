using Fishbone.Core;
using Fishbone.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbone.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext orderDbContext;

        public UnitOfWork(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }

        public void Dispose()
        {
            orderDbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.orderDbContext.SaveChangesAsync();
        }
    }
}
