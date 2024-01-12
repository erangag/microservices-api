using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly OrderContext _dbContext;

        public OrderRepository(OrderContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
