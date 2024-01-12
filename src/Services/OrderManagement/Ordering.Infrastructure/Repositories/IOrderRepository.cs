using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ordering.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
    }
}
