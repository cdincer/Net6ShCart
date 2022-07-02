using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Net6ShCart.Entity.Layer.DAL.Entities
{
    public class ShoppingCartEntity
    {
        
        public long UserID {get; set;}
        public long ProductID {get; set;}
        public string ProductName {get; set;}
        public long Quantity {get; set;}
    }
}