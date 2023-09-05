using Net6ShCart.Entities;

namespace Net6ShCart.Controller.Rules.ItemCheckRulesEngine
{
    public interface IItemCheckRule
    {
        bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity);
    }
}