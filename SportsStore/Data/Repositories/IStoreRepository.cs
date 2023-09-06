using SportsStore.Models;

namespace SportsStore.Data.Repositories
{
    public interface IStoreRepository: IGenaricRepository<Product> 
    {
        public IQueryable<Product> GetAll { get; }

        public Product? GetbyName(string name);
    }
}
