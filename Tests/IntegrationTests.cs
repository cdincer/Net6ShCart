using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Net6ShCart.Controller;
using Net6ShCart.Database;
using Net6ShCart.Database.Repository;
using Net6ShCart.Entities;
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
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
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
            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
            result = items.Count == 3 ? true : false;
            Assert.True(result);
        }

        [Theory]
        [InlineData(14, 1003, "LiterallyPropaneTank", 8)]
        public async Task AddItem(long userID, long productID, string productName, long quantity)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
            ShoppingCartEntity shoppingCartEntity;
            shoppingCartEntity = new ShoppingCartEntity();
            shoppingCartEntity.UserID = userID;
            shoppingCartEntity.ProductID = productID;
            shoppingCartEntity.ProductName = productName;
            shoppingCartEntity.Quantity = quantity;

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
            var ShoppingController = _fixture.GetScopedService<ShoppingCartController>(_testOutputHelper);
            var _ShopCartRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper);
            var _ProductRepo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper);
            var _ProductStockRepo = _fixture.GetScopedService<IProductStockRepository>(_testOutputHelper);

            _ShopCartRepo = new ShoppingCartRepository(context);
            _ProductRepo = new ProductRepository(context);
            _ProductStockRepo = new ProductStockRepository(context);
            ShoppingController = new ShoppingCartController(context, _ShopCartRepo, _ProductStockRepo, _ProductRepo);


            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            ShoppingController.PostEntity(shoppingCartEntity);
            List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
            result = items.Count == 4 ? true : false;

            Assert.True(result);
        }

        [Theory]
        [InlineData(15, 1003, "LiterallyPropaneTank", 13)]
        public async Task RejectItem(long userID, long productID, string productName, long quantity)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
            ShoppingCartEntity shoppingCartEntity;
            shoppingCartEntity = new ShoppingCartEntity();
            shoppingCartEntity.UserID = userID;
            shoppingCartEntity.ProductID = productID;
            shoppingCartEntity.ProductName = productName;
            shoppingCartEntity.Quantity = quantity;

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
            var ShoppingController = _fixture.GetScopedService<ShoppingCartController>(_testOutputHelper);
            var _ShopCartRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper);
            var _ProductRepo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper);
            var _ProductStockRepo = _fixture.GetScopedService<IProductStockRepository>(_testOutputHelper);

            _ShopCartRepo = new ShoppingCartRepository(context);
            _ProductRepo = new ProductRepository(context);
            _ProductStockRepo = new ProductStockRepository(context);
            ShoppingController = new ShoppingCartController(context, _ShopCartRepo, _ProductStockRepo, _ProductRepo);


            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            await ShoppingController.PostEntity(shoppingCartEntity);
            List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
            result = items.Count == 3 ? true : false;

            Assert.True(result);
        }

        [Theory]
        [InlineData(14, 1001, "TestProduct1", 2)]
        public async Task PutItem(long userID, long productID, string productName, long quantity)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
            ShoppingCartEntity shoppingCartEntity;
            shoppingCartEntity = new ShoppingCartEntity();
            shoppingCartEntity.UserID = userID;
            shoppingCartEntity.ProductID = productID;
            shoppingCartEntity.ProductName = productName;
            shoppingCartEntity.Quantity = quantity;

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
            var ShoppingController = _fixture.GetScopedService<ShoppingCartController>(_testOutputHelper);
            var _ShopCartRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper);
            var _ProductRepo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper);
            var _ProductStockRepo = _fixture.GetScopedService<IProductStockRepository>(_testOutputHelper);

            _ShopCartRepo = new ShoppingCartRepository(context);
            _ProductRepo = new ProductRepository(context);
            _ProductStockRepo = new ProductStockRepository(context);
            ShoppingController = new ShoppingCartController(context, _ShopCartRepo, _ProductStockRepo, _ProductRepo);


            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            await ShoppingController.PutEntity(shoppingCartEntity);
            var QuanityExpected = await ShoppingController.GetSingle(userID, productID);
            result = QuanityExpected.Value.Quantity == 2 ? true : false;

            Assert.True(result);
        }

        [Theory]
        [InlineData(14, 1001, "TestProduct1", 2)]
        public async Task RemoveItem(long userID, long productID, string productName, long quantity)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
            ShoppingCartEntity shoppingCartEntity;
            shoppingCartEntity = new ShoppingCartEntity();
            shoppingCartEntity.UserID = userID;
            shoppingCartEntity.ProductID = productID;
            shoppingCartEntity.ProductName = productName;
            shoppingCartEntity.Quantity = quantity;

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
            var ShoppingController = _fixture.GetScopedService<ShoppingCartController>(_testOutputHelper);
            var _ShopCartRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper);
            var _ProductRepo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper);
            var _ProductStockRepo = _fixture.GetScopedService<IProductStockRepository>(_testOutputHelper);

            _ShopCartRepo = new ShoppingCartRepository(context);
            _ProductRepo = new ProductRepository(context);
            _ProductStockRepo = new ProductStockRepository(context);
            ShoppingController = new ShoppingCartController(context, _ShopCartRepo, _ProductStockRepo, _ProductRepo);


            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            await ShoppingController.DeleteEntity(shoppingCartEntity);
            List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
            result = items.Count == 2 ? true : false;
            Assert.True(result);
        }

        [Theory]
        [InlineData(15)]
        public async Task RemoveAllItems(long userID)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
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
            var ShoppingController = _fixture.GetScopedService<ShoppingCartController>(_testOutputHelper);
            var _ShopCartRepo = _fixture.GetScopedService<IShoppingCartRepository>(_testOutputHelper);
            var _ProductRepo = _fixture.GetScopedService<IProductRepository>(_testOutputHelper);
            var _ProductStockRepo = _fixture.GetScopedService<IProductStockRepository>(_testOutputHelper);

            _ShopCartRepo = new ShoppingCartRepository(context);
            _ProductRepo = new ProductRepository(context);
            _ProductStockRepo = new ProductStockRepository(context);
            ShoppingController = new ShoppingCartController(context, _ShopCartRepo, _ProductStockRepo, _ProductRepo);


            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            await ShoppingController.DeleteAll(userID);
            List<ShoppingCartEntity> items = context.ShoppingCartEntities.ToList();
            result = items.Count == 1 ? true : false;
            Assert.True(result);
        }

        [Theory]
        [InlineData(15)]
        public async Task CheckUserCart(long userID)
        {
            bool result = true;
            #region CreateDBConnection,Get Requirements and Seed DB
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
            //Basic template for testing other things with seeding.
            Seed NewItems = new Seed(context);
            NewItems.SeedItems();
            #endregion
            var items = _ShopRepo.GetUserShoppingCart(userID);
            result = items.Result.Value.Count() == 2 ? true : false;
            Assert.True(result);
        }

    }
}