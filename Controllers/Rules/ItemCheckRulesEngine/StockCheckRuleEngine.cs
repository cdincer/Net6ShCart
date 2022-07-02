using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.ItemCheckRulesEngine
{
    public class StockCheckRuleEngine
    {
        List<IItemCheckRule> _rules = new List<IItemCheckRule>();

        public StockCheckRuleEngine(IEnumerable<IItemCheckRule> rules)
        {
            _rules.AddRange(rules);
        }

        public bool CheckStockRules(ShoppingCartEntity shoppingCartEntity)
        {
            bool decision = false;
            foreach(var rule in _rules)
            {
                decision = rule.CalculateItemRule(shoppingCartEntity);
                if(!decision)
                break;
            }

            return decision;
        }
    }
}