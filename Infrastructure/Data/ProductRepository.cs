using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<List<string>> GetBrandsAsync()
    {
        List<string> brands = await context.Products.Select(x => x.Brand).Distinct().ToListAsync();

        return brands;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
       return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<List<string>> GetTypesAsync()
    {
        List<string> types = await context.Products.Select(x => x.Type).Distinct().ToListAsync();
        return types;
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
