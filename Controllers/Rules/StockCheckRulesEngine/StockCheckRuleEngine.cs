using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.StockCheckRulesEngine
{
    public class StockCheckRuleEngine
    {
        List<IStockCheckRule> _rules = new List<IStockCheckRule>();

        public StockCheckRuleEngine(IEnumerable<IStockCheckRule> rules)
        {
            _rules.AddRange(rules);
        }

        public bool CheckStockRules(ShoppingCartEntity shoppingCartEntity)
        {
            bool decision = false;
            foreach(var rule in _rules)
            {
                decision = rule.CalculateStockRule(shoppingCartEntity);
            }

            return decision;
        }
    }
}