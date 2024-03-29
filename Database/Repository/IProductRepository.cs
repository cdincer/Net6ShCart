using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entities;

namespace Net6ShCart.Database.Repository
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProductEntity(long ProductID);
        Task<ProductEntity> AddProductEntity(ProductEntity AddProductEntity);
    }
}