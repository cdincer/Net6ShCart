using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public class ProductStockRepository : IProductStockRepository
    {

        private readonly ShoppingCartContext _context;
        public ProductStockRepository(ShoppingCartContext context)
        {
            this._context = context;
        }
        public async Task<ActionResult<ProductStockEntity>> GetProductStock(long ProductID)
        {
             var items = _context.ProductStockEntities.FirstOrDefault(c=>c.ProductID == c.ProductID);

            return items;        
        }

        public async Task<ActionResult<ProductStockEntity>> AddProductStock(ProductStockEntity ItemToAdd)
        {
            if (_context.ShoppingCartEntities == null)
            {
               return  null;
            }
            _context.ProductStockEntities.Add(ItemToAdd);
            await _context.SaveChangesAsync();

            return ItemToAdd;
        }
    }
}