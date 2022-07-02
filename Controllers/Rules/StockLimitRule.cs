using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules
{
    public class StockLimitRule : IStockCheckRule
    {
        //Not allowed to get more than 4 in this productCategory.
        long _StockQuantity = 4;
        public bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity, long StockQuantity)
        {
            StockQuantity = _StockQuantity;
            if(ShoppingCartEntity.Quantity > StockQuantity)
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