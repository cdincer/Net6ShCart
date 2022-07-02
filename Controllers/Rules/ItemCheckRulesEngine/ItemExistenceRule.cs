using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers.Rules.ItemCheckRulesEngine
{
    public class ItemExistenceRule : IItemCheckRule
    {   private readonly IProductRepository _ProductRepo;
        public ItemExistenceRule(IProductRepository productrepo)
        {
            _ProductRepo = productrepo;
        }

        public bool CalculateItemRule(ShoppingCartEntity ShoppingCartEntity)
        {
            var productEntity = _ProductRepo.GetProductEntity(ShoppingCartEntity.ProductID);
            if (productEntity.Result == null)
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