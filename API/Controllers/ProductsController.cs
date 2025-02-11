using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _storeContext;
    public ProductsController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _storeContext.Products.ToListAsync());
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return Ok(await _storeContext.Products.FindAsync(id));
    }

    [HttpPost("product")]
    public async Task<ActionResult> CreateProduct([FromBody]Product product)
    {
        _storeContext.Products.Add(product);
        await _storeContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody]Product product)
    {
        var productToUpdate = await _storeContext.Products.FindAsync(product.Id);
        if (productToUpdate == null) return NotFound();
        productToUpdate = product;
       // _storeContext.Entry(product).State = EntityState.Modified;
        await _storeContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("id:int")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var productToDelete = await _storeContext.Products.FindAsync(id);
        if (productToDelete == null) return NotFound();
        _storeContext.Products.Remove(productToDelete);
        await _storeContext.SaveChangesAsync();
        return Ok();
    }
}
