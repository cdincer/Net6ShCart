using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entities;

namespace Net6ShCart.DataLayer.ShoppingCart
{
    public class ProductStockRepository : IProductStockRepository
    {

        private readonly ShoppingCartContext _context;
        public ProductStockRepository(ShoppingCartContext context)
        {
            this._context = context;
        }
        public async Task<ActionResult<long>> GetProductStock(long ProductID)
        {
            var WarehouseList = _context.ProductStockEntities.Select(x => new { x.ProductWareHouseID }).Distinct().ToList();
            long sumStock = 0;
            foreach (var item in WarehouseList)
            {
                ProductStockEntity goBetween = new ProductStockEntity();
                goBetween = await _context.ProductStockEntities.FindAsync(ProductID, item.ProductWareHouseID);
                sumStock += goBetween != null ? goBetween.ProductStock : 0;
            }
            return sumStock;
        }

        public async Task<ActionResult<ProductStockEntity>> AddProductStock(ProductStockEntity ItemToAdd)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            _context.ProductStockEntities.Add(ItemToAdd);
            await _context.SaveChangesAsync();

            return ItemToAdd;
        }
    }
}