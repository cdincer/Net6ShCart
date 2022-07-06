using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Net6ShCart.DataLayer.ShoppingCart
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
            try{
            if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            _context.ShoppingCartEntities.Add(ItemToAdd);
            await _context.SaveChangesAsync();
            Log.Information("Item Added Succesfuly");
            }
              catch (Exception ex)
            {
                Log.Fatal(ex, "Add Failed");
            }
            //    finally
            // {
            //     Log.Information("Shut down complete");
            //     Log.CloseAndFlush();
            // } 

            return ItemToAdd;
        }

        public async Task<IActionResult> DeleteItemShoppingCart(ShoppingCartEntity ItemToRemove)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            var RemoveEntity = await _context.ShoppingCartEntities.FindAsync(ItemToRemove.UserID, ItemToRemove.ProductID);
            if (RemoveEntity == null)
            {
                return null;
            }
            _context.ShoppingCartEntities.Remove(RemoveEntity);
            await _context.SaveChangesAsync();

           return null;
        }

          public async Task<IActionResult> DeleteAllShoppingCart(long UserID)
        {
              if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            var RemoveEntity =  _context.ShoppingCartEntities.Where(c=> c.UserID == UserID).ToList();
            if (RemoveEntity == null)
            {
                return null;
            }
            _context.ShoppingCartEntities.RemoveRange(RemoveEntity);
            await _context.SaveChangesAsync();

           return null;
        }

        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetAllItemShoppingCart()
        {
            var shoppingCartEntity = await _context.ShoppingCartEntities.ToListAsync();

            return shoppingCartEntity;
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

        public async Task<ActionResult<ShoppingCartEntity>> UpdateItemShoppingCart(ShoppingCartEntity ItemToUpdate)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return null;
            }
            var replace = await _context.ShoppingCartEntities.FirstAsync(c => c.UserID == ItemToUpdate.UserID && c.ProductID == ItemToUpdate.ProductID);
            replace.Quantity = ItemToUpdate.Quantity;
            await _context.SaveChangesAsync();

            return ItemToUpdate;
        }
    }
}