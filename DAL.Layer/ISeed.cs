using Net6ShCart.Entity.Layer.DAL.Entities;
using Newtonsoft.Json;
using Net6ShCart.DAL.Layer.ShoppingCart;

namespace Net6ShCart.DAL.Layer
{
    public interface ISeed
    {
     void SeedItems();
    }
}