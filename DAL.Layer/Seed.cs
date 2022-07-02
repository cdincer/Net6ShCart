using Net6ShCart.Entity.Layer.DAL.Entities;
using Newtonsoft.Json;
using Net6ShCart.DAL.Layer.ShoppingCart;

namespace Net6ShCart.DAL.Layer
{
    public class Seed
    {
        public static void SeedItems(ShoppingCartContext context)
        {
            var seedData = System.IO.File.ReadAllText("ShoppingCartSeedData.json");
            var ShoppingCartItems = JsonConvert.DeserializeObject<List<ShoppingCartEntity>>(seedData);
            ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(context);

            foreach(var item in ShoppingCartItems)
            {
                shoppingCartRepository.AddItemShoppingCart(item);
            }
        }
    }
}