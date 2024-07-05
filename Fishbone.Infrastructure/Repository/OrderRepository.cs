using Fishbone.Core;
using Fishbone.Core.Dto;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext=orderDbContext;
        }
        public async Task<int> CountAsync()
        {
            return await orderDbContext.Orders.CountAsync();
        }

        public async Task<List<Order>> GetAllAsync(int page, int size)
        {
            var foundOrders = await orderDbContext.Orders
                .OrderBy(Orders => Orders.OrderDate)
                .Include(Orders => Orders.Product)
                .Include(Orders => Orders.User)
                .Skip((page - 1) * size).Take(size)
                .AsNoTracking()
                .ToListAsync();
            return foundOrders;
        }

        public async Task<Order> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid task item ID.", nameof(id));
            }
            var foundOrders = await orderDbContext.Orders
                .Include(Orders => Orders.User)
                .Include(Orders => Orders.Product)
                .FirstOrDefaultAsync(task => task.Id == id);
            if (foundOrders == null)
            {
                throw new ArgumentNullException($"Task item with ID {id} not found.");
            }
            return foundOrders;
        }

        public async Task<Int64> InsertAsync(OrderModifiedDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "order cannot be null");
            }
            if (order.OrderQuantity < 0)
            {
                throw new ArgumentException("Order Quantitymusbe be > 0.");
            }
            var neworder = new Order
            {
                UserId = order.UserId,
                ProductId = order.ProductId,
                OrderQuantity=order.OrderQuantity,
                OrderTotal = order.OrderTotal,
                OrderDate = DateTime.UtcNow.Date,
                OrderDescription = order.OrderDescription,
             };
            await orderDbContext.Orders.AddAsync(neworder);
            return neworder.Id;
        }

        public async Task<int> RemoveAsync(int id)
        {
            if (id > 0)
            {
                var order = await orderDbContext.Orders.FindAsync(id);

                if (order != null)
                {
                    orderDbContext.Orders.Remove(order);
                    return id;
                }
                else
                {
                    throw new ArgumentException("order not found", nameof(id));
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }
        }

        public async Task<Order> UpdateAsync(OrderModifiedDto order)
        {
            if (order != null)
            {
                var _order = await orderDbContext.Orders.FindAsync(order.Id);
                if (_order != null)
                {
                    _order.UserId = order.UserId;
                    _order.OrderDate = order.OrderDate;
                    _order.ProductId = order.ProductId;
                    _order.UserId = order.UserId;
                    _order.OrderQuantity = order.OrderQuantity;
                    _order.OrderTotal = order.OrderTotal;
                    return _order;
                }
                else
                {
                    throw new ArgumentException("TaskItem not found", nameof(order));
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(order), "TaskItem cannot be null");
            }
        }
    }
}
