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
            var seedData = System.IO.File.ReadAllText("DAL.Layer/ShoppingCartSeedData.json");
            var ShoppingCartItems = JsonConvert.DeserializeObject<List<ShoppingCartEntity>>(seedData);
            ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(_context);

            foreach(var item in ShoppingCartItems)
            {
                shoppingCartRepository.AddItemShoppingCart(item);
            }
        }
    }
}