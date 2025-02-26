using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(string? brand, string? type);
    Task<Product?> GetProductByIdAsync(int id);
    Task<List<string>> GetBrandsAsync();
    Task<List<string>> GetTypesAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
