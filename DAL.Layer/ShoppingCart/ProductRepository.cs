using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingCartContext _context;

        public ProductRepository(ShoppingCartContext shoppingCartContext)
        {
            _context = shoppingCartContext;
        }

        public async Task<ProductEntity> AddProductEntity(ProductEntity AddProductEntity)
        {
            _context.ProductEntities.Add(AddProductEntity);
            await _context.SaveChangesAsync();
            return AddProductEntity;
        }

        public async Task<ProductEntity> GetProductEntity(long ProductID)
        {
            var productEntity = await _context.ProductEntities.FindAsync(ProductID);
            return productEntity;
        }
    }
}