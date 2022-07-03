using System.ComponentModel.DataAnnotations;
namespace Net6ShCart.Entities
{
    public class ProductEntity
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductCateGoryID {get; set;}
    }
}