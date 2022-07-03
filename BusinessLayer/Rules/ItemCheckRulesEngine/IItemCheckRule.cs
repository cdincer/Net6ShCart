using Net6ShCart.Entities;

namespace Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine
{
    public interface IItemCheckRule
    {
        bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity);
    }
}