using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? brand, string? type) : base(p => 
        (string.IsNullOrEmpty(brand) || p.Brand == brand) &&
        (string.IsNullOrEmpty(type) || p.Type == type)
    )
    {
        
    }
}
