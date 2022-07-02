using System.ComponentModel.DataAnnotations;

namespace Net6ShCart.Entity.Layer.DAL.Entities
{
    public class ProductStockEntity
    {

        public long ProductID {get; set;}
        public long ProductWareHouseID{get; set;}
        public long ProductStock{get; set;}
        
    }
}