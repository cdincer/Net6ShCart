using Microsoft.EntityFrameworkCore;
using Net6ShCart.DAL.Layer;
using Net6ShCart.Entity.Layer.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Net6ShCart.DAL.Layer.ShoppingCart;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ShoppingCartContext>(opt =>
            opt.UseInMemoryDatabase("ShoppingCartItems"));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ISeed,Seed>();
        builder.Services.AddScoped<IShoppingCartRepository,ShoppingCartRepository>();
        builder.Services.AddScoped<IProductStockRepository,ProductStockRepository>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

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