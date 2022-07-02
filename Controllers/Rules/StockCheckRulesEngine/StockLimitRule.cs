using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.StockCheckRulesEngine
{
    public class StockLimitRule : IStockCheckRule
    {
        //Not allowed to get more than 4 in this productCategory.
      
        public bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity)
        {
            if(ShoppingCartEntity.Quantity > 4)
            {
                return false;
            }
            else
            {
            return true;
            }
        }
    }
}