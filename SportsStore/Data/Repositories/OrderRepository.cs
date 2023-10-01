using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _dbContext;

    public OrderRepository(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Order> Orders => _dbContext.orders.Include(o => o.Lines).ThenInclude(o => o.Product);

    void IOrderRepository.SaveChanges(Order order)
    {
        _dbContext.AttachRange(order.Lines.Select(p => p.Product));
        _dbContext.orders.Add(order);
        _dbContext.SaveChanges();
    }
}
