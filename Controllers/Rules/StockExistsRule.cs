using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules
{
    public class StockExistsRule : IStockCheckRule
    {
        public bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity,long StockQuantity)
        {
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