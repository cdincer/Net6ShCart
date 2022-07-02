using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public interface IProductStockRepository
    {
        Task<ActionResult<long>> GetProductStock(long ProductID);
        Task<ActionResult<ProductStockEntity>> AddProductStock(ProductStockEntity ItemToAdd);
    }
}