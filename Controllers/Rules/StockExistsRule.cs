using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules
{
 
    public class StockExistsRule : IStockCheckRule
    {
        private readonly IProductStockRepository _StockRepo;
         public StockExistsRule(IProductStockRepository stockrepo)
        {         
            _StockRepo = stockrepo;
        }

        public bool CalculateStockRule(ShoppingCartEntity ShoppingCartEntity)
        {
           var item = _StockRepo.GetProductStock(ShoppingCartEntity.ProductID);
           if(ShoppingCartEntity.Quantity > item.Result.Value.ProductStock)
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