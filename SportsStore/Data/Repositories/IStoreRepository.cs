﻿using SportsStore.Models;

namespace SportsStore.Data.Repositories
{
    public interface IStoreRepository : IGenaricRepository<Product>
    {
        public void SaveProduct(Product product);
    }
}
