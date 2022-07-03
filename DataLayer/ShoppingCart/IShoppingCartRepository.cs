using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Entities;

namespace Net6ShCart.DataLayer.ShoppingCart
{
    public interface IShoppingCartRepository
    {
        Task<ActionResult<ShoppingCartEntity>> AddItemShoppingCart(ShoppingCartEntity ItemToAdd);
        Task<ActionResult<ShoppingCartEntity>> GetItemShoppingCart(long UserID,long ProductID);
        Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetAllItemShoppingCart();
        Task<ActionResult<ShoppingCartEntity>> UpdateItemShoppingCart(ShoppingCartEntity ItemToUpdate);
        Task<IActionResult> DeleteItemShoppingCart(ShoppingCartEntity ItemToUpdate);
        Task<IActionResult> DeleteAllShoppingCart(long UserID);
    }
    
}