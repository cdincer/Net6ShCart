using System.ComponentModel.DataAnnotations;

namespace Net6ShCart.Entities
{
    public class ProductStockEntity
    {

        public long ProductID {get; set;}
        public long ProductWareHouseID{get; set;}
        public long ProductStock{get; set;}
        
    }
}