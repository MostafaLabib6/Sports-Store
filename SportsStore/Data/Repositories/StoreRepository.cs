using SportsStore.Models;

namespace SportsStore.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _dbContext;

        public StoreRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> GetAll => _dbContext.Products;
    }
}
