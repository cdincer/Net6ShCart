using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Controller.Rules.ItemCheckRulesEngine;
using Net6ShCart.Database.Repository;
using Net6ShCart.Entities;
using Serilog;

namespace Net6ShCart.Controller.Rules.ItemCheckRulesEngine
{

    public class StockExistsRule : IItemCheckRule
    {
        private readonly IProductStockRepository _StockRepo;
        public StockExistsRule(IProductStockRepository stockrepo)
        {
            _StockRepo = stockrepo;
        }

        public bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity)
        {
            try
            {
                var item = _StockRepo.GetProductStock(ShoppingCartEntity.ProductID);
                if (ShoppingCartEntity.Quantity > item.Result.Value)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Stock Existence Rule Failed");
                return false;
            }
        }
    }
}