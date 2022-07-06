using Microsoft.EntityFrameworkCore;
using Net6ShCart.DataLayer;
using Net6ShCart.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Net6ShCart.DataLayer.ShoppingCart;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/ShCartLogs.txt", rollingInterval: RollingInterval.Day)
        .CreateBootstrapLogger();

        Log.Information("Starting up");


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ShoppingCartContext>(opt =>
            opt.UseInMemoryDatabase("ShoppingCartItems"));

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(7294); // to listen for incoming http connection on port 7294
                                       //  options.ListenAnyIP(5127, configure => configure.UseHttps()); // to listen for incoming https connection on port 7001
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ISeed, Seed>();
        builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        builder.Services.AddScoped<IProductStockRepository, ProductStockRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }


        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<ISeed>();
            dbInitializer.SeedItems();
        }

        app.Run();
    }
}