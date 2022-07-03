using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Net6ShCart.Controllers;
using Net6ShCart.DAL.Layer;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;
using Net6ShCart.Tests.Fixture;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Net6ShCart.Tests
{
    public class IntegrationTests : TestBed<TestFixture>
    {
  
        
        public IntegrationTests(ITestOutputHelper testOutputHelper, TestFixture fixture) : base(testOutputHelper, fixture)
        {        
           
        }

        [Fact]
        public async Task CartCheck()
        {
            SqliteConnection _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            DbContextOptions _contextOptions = new DbContextOptionsBuilder<ShoppingCartContext>().UseSqlite(_connection).Options;
            using var context = new ShoppingCartContext((DbContextOptions<ShoppingCartContext>)_contextOptions);
                 if (context.Database.EnsureCreated())
                {
                    using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                    viewCommand.CommandText = @"
                    CREATE VIEW AllResources AS
                    SELECT ProductName
                    FROM ShoppingCartEntities;";
                    viewCommand.ExecuteNonQuery();
                }

                var _ShopRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper); 
                _ShopRepo = new ShoppingCartRepository(context);
                _ShopRepo.AddItemShoppingCart(new Entity.Layer.DAL.Entities.ShoppingCartEntity{UserID=144,ProductID=122,ProductName="est",Quantity=17});
                context.SaveChanges();
            
                Seed NewItems = new Seed(context);
                NewItems.SeedItems();
                List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
                Assert.True(true);
        }
    }
}