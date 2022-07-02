using System.ComponentModel.DataAnnotations;
namespace Net6ShCart.Entity.Layer.DAL.Entities
{
    public class ProductEntity
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}