using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DataLayer.ShoppingCart;
using Net6ShCart.Entities;

namespace Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine
{
    public class StockCalculator
    {
        private readonly IProductStockRepository _StockRepo;
        private readonly IProductRepository _ProductRepo;

        public StockCalculator(IProductStockRepository stockrepo,IProductRepository productRepo)
        {
            _StockRepo = stockrepo;
            _ProductRepo=productRepo;
        }


        public bool CalculateStock(ShoppingCartEntity shoppingCartEntity)
        {
            var rules = new List<IItemCheckRule>();
            rules.Add(new ItemExistenceRule(_ProductRepo));
            rules.Add(new StockExistsRule(_StockRepo));
            rules.Add(new StockLimitRule(_ProductRepo));

            var engine = new StockCheckRuleEngine(rules);
            return engine.CheckStockRules(shoppingCartEntity);
        }
    }
}