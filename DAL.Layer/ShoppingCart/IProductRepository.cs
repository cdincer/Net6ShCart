using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public interface IProductRepository
    {
         Task<ProductEntity> GetProductEntity(long ProductID);
         Task<ProductEntity> AddProductEntity(ProductEntity AddProductEntity);
    }
}