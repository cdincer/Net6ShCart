using Net6ShCart.Entities;
using Newtonsoft.Json;


namespace Net6ShCart.Database
{
    public interface ISeed
    {
        void SeedItems();
    }
}