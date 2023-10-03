using SportsStore.Models;
using System.Linq.Expressions;

namespace SportsStore.Data.Repositories
{
    public interface IGenaricRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T Get(Expression<Func<T, bool>> filter);

        public void Update(T entity);
        public void Delete(T entity);
        public void Add(T entity);
        public void RemoveRange(IEnumerable<T> entities);
        public void SaveChanges(T entity);
    }
}
