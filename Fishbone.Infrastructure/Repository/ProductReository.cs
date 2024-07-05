using Fishbone.Core;
using Fishbone.Core.Entities;
using Fishbone.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fishbone.Infrastructure.Repository
{
    public class ProductReository : IProductRepository
    {
        private readonly OrderDbContext orderDbContext;

        public ProductReository(OrderDbContext orderDbContext)
        {
            this.orderDbContext=orderDbContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await orderDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product ID.", nameof(id));
            }
            var result = await orderDbContext.Products.FindAsync(id);
            return result;
        }

        public async Task<Int64> InsertAsync(Product product)
        {
            await orderDbContext.Products.AddAsync(product);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Subject cannot be null.");
            }
            await orderDbContext.Products.AddAsync(product);
            return product.Id;
        }
    }
}
