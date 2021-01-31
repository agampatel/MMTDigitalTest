using Microsoft.EntityFrameworkCore;
using MMT.CustomerOrder.DataSource;
using MMT.CustomerOrder.Interfaces;
using MMT.CustomerOrder.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder.Core
{
    public interface IOrderRepository:IRepository<Order>
    {
        Task<Models.Order> GetLatestOrderAsync(string customerId);
    }
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MMTDigitalContext context):base(context)
        {

        }
        public async Task<Order> GetLatestOrderAsync(string customerId)
        {
            return await Context.Set<Order>().Include(x=>x.OrderItems).ThenInclude(x=>x.Product).OrderByDescending(o => o.OrderDate).FirstOrDefaultAsync(o => o.CustomerId == customerId);
            
        }
    }
}
