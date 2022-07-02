using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entity.Layer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly ShoppingCartContext _context;
        public ShoppingCartRepository(ShoppingCartContext context)
        {
            this._context = context;
        }

        public async Task<ActionResult<ShoppingCartEntity>> AddItemShoppingCart(ShoppingCartEntity ItemToAdd)
        {
            if (_context.ShoppingCartEntities == null)
            {
             return  null;
            }
            _context.ShoppingCartEntities.Add(ItemToAdd);
            await _context.SaveChangesAsync();

            return ItemToAdd;
        }

        public Task<IActionResult> DeleteAllShoppingCart()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteItemShoppingCart(ShoppingCartEntity ItemToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ShoppingCartEntity>> GetAllItemShoppingCart()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ShoppingCartEntity>?> GetItemShoppingCart(long UserID, long ProductID)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            var shoppingCartEntity = await _context.ShoppingCartEntities.FindAsync(UserID, ProductID);

            if (shoppingCartEntity == null)
            {
                return null;
            }

            return shoppingCartEntity;
        }

        public Task<ActionResult<ShoppingCartEntity>> UpdateItemShoppingCart(ShoppingCartEntity ItemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}