using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        try
        {
            if (!context.Products.Any())
            {
                var products = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var productsList = JsonSerializer.Deserialize<List<Product>>(products);

                context.Products.AddRange(productsList);

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {

        }
    }
}
