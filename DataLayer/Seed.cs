using Net6ShCart.Entities;
using Newtonsoft.Json;
using Net6ShCart.DataLayer;
using Net6ShCart.DataLayer.ShoppingCart;
using Serilog;

namespace Net6ShCart.DataLayer
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
            try{
            var seedData = System.IO.File.ReadAllText("DataLayer/SeedJsonFiles/ShoppingCartSeedData.json");
            var ShoppingCartItems = JsonConvert.DeserializeObject<List<ShoppingCartEntity>>(seedData);
            ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(_context);

            foreach(var item in ShoppingCartItems)
            {
                shoppingCartRepository.AddItemShoppingCart(item);
            }
            
            seedData = System.IO.File.ReadAllText("DataLayer/SeedJsonFiles/ProductWareHouseSeedData.json");
            var StockWarehouseItems = JsonConvert.DeserializeObject<List<ProductStockEntity>>(seedData);
            ProductStockRepository productStockRepository = new ProductStockRepository(_context);
            foreach(var item in StockWarehouseItems)
            {
                productStockRepository.AddProductStock(item);
            }

             seedData = System.IO.File.ReadAllText("DataLayer/SeedJsonFiles/ProductSeedData.json");
            var ProductItems = JsonConvert.DeserializeObject<List<ProductEntity>>(seedData);
            ProductRepository productRepository = new ProductRepository(_context);
            foreach(var item in ProductItems)
            {
                productRepository.AddProductEntity(item);
            }
                Log.Information("Seed Succesful");
            }
              catch (Exception ex)
            {
                Log.Fatal(ex, "Seeding Failed");
            }
            //    finally
            // {
            //     Log.Information("Shut down complete");
            //     Log.CloseAndFlush();
            // } 
        }
    }
}