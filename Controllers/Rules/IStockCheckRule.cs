using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules
{
    public interface IStockCheckRule
    {
        bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity,long StockQuantity);
    }
}