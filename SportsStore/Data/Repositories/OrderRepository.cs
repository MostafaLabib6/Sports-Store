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
        _dbContext.Orders.Add(entity);
    }

    public void Delete(Order entity)
    {
        _dbContext?.Orders.Remove(entity);
    }

    public Order Get(Expression<Func<Order, bool>> filter)
    {
        return _dbContext.Orders.FirstOrDefault(filter)!;
    }

    public IQueryable<Order> GetAll()
    {
        return _dbContext.Orders.Include(o => o.Lines).ThenInclude(o => o.Product);
    }

    public void RemoveRange(IEnumerable<Order> entities)
    {
        _dbContext.RemoveRange(entities);
    }

    public void Update(Order entity)
    {
        _dbContext.Update(entity);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
    public void SaveOrder(Order order)
    {
        _dbContext.AttachRange(order.Lines.Select(p => p.Product));
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

}
