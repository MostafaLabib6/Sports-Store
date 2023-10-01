using SportsStore.Models;

namespace SportsStore.Data.Repositories;

public interface IOrderRepository
{
    public IQueryable<Order> Orders { get;}
    public void SaveChanges (Order order);
}
