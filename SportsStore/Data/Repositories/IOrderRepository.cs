using SportsStore.Models;

namespace SportsStore.Data.Repositories;

public interface IOrderRepository : IGenaricRepository<Order>
{
        public void SaveOrder(Order order);

}
