using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace  Net6ShCart.DAL.Layer
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingCartEntity> GetShoppingCartItems { get; set; } = null!;
    }
}