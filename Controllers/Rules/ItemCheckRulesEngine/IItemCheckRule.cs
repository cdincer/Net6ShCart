using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.ItemCheckRulesEngine
{
    public interface IItemCheckRule
    {
        bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity);
    }
}