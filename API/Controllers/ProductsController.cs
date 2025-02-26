using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        return Ok(await productRepository.GetProductsAsync(brand, type, sort));
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await productRepository.GetProductByIdAsync(id);

        if (product == null) return NotFound();
        return Ok(product);

    }

    [HttpPost("product")]
    public async Task<ActionResult> CreateProduct([FromBody] Product product)
    {
        productRepository.AddProduct(product);

        try
        {
            if (await productRepository.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Unable to create Product");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] Product product)
    {
        if (!productRepository.ProductExists(product.Id)) return NotFound();
        productRepository.UpdateProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Unable to update Product");
    }

    [HttpDelete("id:int")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product? product = await productRepository.GetProductByIdAsync(id);

        if (product == null) return NotFound();
        productRepository.DeleteProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Unable to delete Product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        return Ok(await productRepository.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        return Ok(await productRepository.GetTypesAsync());
    }
}
