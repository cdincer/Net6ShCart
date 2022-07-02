using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.ItemCheckRulesEngine
{
    public class StockLimitRule : IItemCheckRule
    {
        //Not allowed to get more than certain amount,in this product category.
        private readonly IProductRepository _ProductRepo;
        long CertainCategoryID = 9999; 
        public StockLimitRule(IProductRepository productrepo)
        {
            _ProductRepo = productrepo;
        }

        public bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity)
        {
            var productEntity = _ProductRepo.GetProductEntity(ShoppingCartEntity.ProductID);
            if (ShoppingCartEntity.Quantity > 12 &&  CertainCategoryID == productEntity.Result.ProductCateGoryID)
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