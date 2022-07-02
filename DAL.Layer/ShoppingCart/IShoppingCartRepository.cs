using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.DAL.Layer.ShoppingCart
{
    public interface IShoppingCartRepository
    {
         Task<ActionResult<ShoppingCartEntity>> AddItemShoppingCart(ShoppingCartEntity ItemToAdd);
         Task<ActionResult<ShoppingCartEntity>> GetItemShoppingCart(long UserID,long ProductID);
         Task<ActionResult<ShoppingCartEntity>> GetAllItemShoppingCart();
         Task<ActionResult<ShoppingCartEntity>> UpdateItemShoppingCart(ShoppingCartEntity ItemToUpdate);
         Task<IActionResult> DeleteItemShoppingCart(ShoppingCartEntity ItemToUpdate);
        Task<IActionResult> DeleteAllShoppingCart();
    }
    
}