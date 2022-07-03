using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Net6ShCart.Entities;

namespace  Net6ShCart.DataLayer
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartEntity>()
                .HasKey(c => new { c.UserID, c.ProductID });
            modelBuilder.Entity<ProductStockEntity>()
                .HasKey(c => new { c.ProductID, c.ProductWareHouseID });
        }

        public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; } = null!;
        public DbSet<ProductStockEntity> ProductStockEntities {get; set;} = null!;
        public DbSet<ProductEntity> ProductEntities {get; set;} = null;
    }
}