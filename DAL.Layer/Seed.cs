using Net6ShCart.Entity.Layer.DAL.Entities;
using Newtonsoft.Json;
using Net6ShCart.DAL.Layer.ShoppingCart;

namespace Net6ShCart.DAL.Layer
{
    public class Seed : ISeed
    {


         private readonly ShoppingCartContext _context;
        public Seed(ShoppingCartContext context)
        {
            this._context = context;
        }

        public  void SeedItems()
        {
            var seedData = System.IO.File.ReadAllText("DAL.Layer/SeedJsonFiles/ShoppingCartSeedData.json");
            var ShoppingCartItems = JsonConvert.DeserializeObject<List<ShoppingCartEntity>>(seedData);
            ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(_context);

            foreach(var item in ShoppingCartItems)
            {
                shoppingCartRepository.AddItemShoppingCart(item);
            }
            
            seedData = System.IO.File.ReadAllText("DAL.Layer/SeedJsonFiles/ProductWareHouseSeedData.json");
            var StockWarehouseItems = JsonConvert.DeserializeObject<List<ProductStockEntity>>(seedData);
            ProductStockRepository productStockRepository = new ProductStockRepository(_context);
            foreach(var item in StockWarehouseItems)
            {
                productStockRepository.AddProductStock(item);
            }
        }
    }
}