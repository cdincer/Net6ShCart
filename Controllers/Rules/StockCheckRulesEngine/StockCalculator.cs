using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.StockCheckRulesEngine
{
    public class StockCalculator
    {
        private readonly IProductStockRepository _StockRepo;

        public StockCalculator(IProductStockRepository stockrepo)
        {
            _StockRepo = stockrepo;
        }


        public bool CalculateStock(ShoppingCartEntity shoppingCartEntity)
        {
            var rules = new List<IStockCheckRule>();
            rules.Add(new StockExistsRule(_StockRepo));
            rules.Add(new StockLimitRule());

            var engine = new StockCheckRuleEngine(rules);
            return engine.CheckStockRules(shoppingCartEntity);
        }
    }
}