using SportsStore.Models;
using System.Linq.Expressions;

namespace SportsStore.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _dbContext;

        public StoreRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Product entity)
        {
            _dbContext.Products.Add(entity);
        }

        public void Delete(Product entity)
        {
            _dbContext.Products.Remove(entity);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return _dbContext.Products.FirstOrDefault(filter)!;
        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void Update(Product entity)
        {
            _dbContext.Update(entity);
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public void SaveProduct(Product product)
        {
            _dbContext.SaveChanges();
        }
    }
}
