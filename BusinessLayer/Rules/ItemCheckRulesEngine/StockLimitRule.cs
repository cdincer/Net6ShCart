using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DataLayer.ShoppingCart;
using Net6ShCart.Entities;
using Serilog;

namespace Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine
{
    public class StockLimitRule : IItemCheckRule
    {
        //Not allowed to get more than certain amount,in this product category.
        private readonly IProductRepository _ProductRepo;
        long CertainCategoryID = 9999; //Normally you would get this from a cached table,Ignore this magic string. I really don't do things like this on a real project.
        public StockLimitRule(IProductRepository productrepo)
        {
            _ProductRepo = productrepo;
        }

        public bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity)
        {
            try
            {
                var productEntity = _ProductRepo.GetProductEntity(ShoppingCartEntity.ProductID);
                if (ShoppingCartEntity.Quantity > 12 && CertainCategoryID == productEntity.Result.ProductCateGoryID)
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
                Log.Fatal(ex, "Stock Limit Rule Failed");
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