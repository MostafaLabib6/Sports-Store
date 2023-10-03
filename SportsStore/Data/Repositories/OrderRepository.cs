using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using System.Linq.Expressions;

namespace SportsStore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _dbContext;

    public OrderRepository(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order entity)
    {
        _dbContext.orders.Add(entity);
    }

    public void Delete(Order entity)
    {
        _dbContext?.orders.Remove(entity);
    }

    public Order Get(Expression<Func<Order, bool>> filter)
    {
        return _dbContext.orders.FirstOrDefault(filter)!;
    }

    public IEnumerable<Order> GetAll()
    {
        return _dbContext.orders.Include(o => o.Lines).ThenInclude(o => o.Product);
    }

    public void RemoveRange(IEnumerable<Order> entities)
    {
        _dbContext.RemoveRange(entities);
    }

    public void Update(Order entity)
    {
        _dbContext.Update(entity);
    }

    public void SaveChanges(Order order)
    {
        _dbContext.AttachRange(order.Lines.Select(p => p.Product));
        _dbContext.orders.Add(order);
        _dbContext.SaveChanges();
    }

}
