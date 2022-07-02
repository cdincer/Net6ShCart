using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.StockCheckRulesEngine
{
    public interface IStockCheckRule
    {
        bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity);
    }
}