using System.ComponentModel.DataAnnotations;

namespace Net6ShCart.Entity.Layer.DAL.Entities
{
    public class ShoppingCartEntity
    {
        [Key]
        public long UserID {get; set;}
        public long ProductID {get; set;}
        public string ProductName {get; set;}
        public long Quantity {get; set;}
    }
}