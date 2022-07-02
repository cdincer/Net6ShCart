using System.ComponentModel.DataAnnotations;

namespace Net6ShCart.Entity.Layer.DAL.Entities
{
    public class ProductStockEntity
    {
        [Key]
        public long ProductID {get; set;}
        public long ProductWareHouseID{get; set;}
        public long ProductStock{get; set;}
        
    }
}