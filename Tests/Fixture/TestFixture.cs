using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net6ShCart.Database;
using Net6ShCart.Database.Repository;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Net6ShCart.Tests.Fixture
{
    public class TestFixture : TestBedFixture
    {
        protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
            => services.AddDbContext<ShoppingCartContext>(opt =>
                opt.UseInMemoryDatabase("ShoppingCartItems"))
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductStockRepository, ProductStockRepository>()
                .AddTransient<IShoppingCartRepository, ShoppingCartRepository>()
                .AddTransient<ISeed, Seed>()
                .AddControllers()
                ;


        protected override ValueTask DisposeAsyncCore()
            => new();

        protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        {
            yield return new() { Filename = "appsettings.json", IsOptional = false };
        }
    }
}