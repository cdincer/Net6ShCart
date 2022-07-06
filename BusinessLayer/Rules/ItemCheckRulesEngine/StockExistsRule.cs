using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine;
using Net6ShCart.DataLayer.ShoppingCart;
using Net6ShCart.Entities;
using Serilog;

namespace Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine
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
            // finally
            // {
            //     Log.Information("Shut down complete");
            //     Log.CloseAndFlush();
            // }

        }
    }
}