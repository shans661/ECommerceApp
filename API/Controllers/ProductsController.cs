using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class ProductsController(IGenericRepository<Product> genericRepository) : ControllerBase
{
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await genericRepository.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await genericRepository.GetByIdAsync(id);

        if (product == null) return NotFound();
        return Ok(product);

    }

    [HttpPost("product")]
    public async Task<ActionResult> CreateProduct([FromBody] Product product)
    {
        genericRepository.Add(product);

        try
        {
            if (await genericRepository.SaveChangesAsync())
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
        if (!genericRepository.Exists(product.Id)) return NotFound();
        genericRepository.Update(product);

        if (await genericRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Unable to update Product");
    }

    [HttpDelete("id:int")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product? product = await genericRepository.GetByIdAsync(id);

        if (product == null) return NotFound();
        genericRepository.Delete(product);

        if (await genericRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Unable to delete Product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        //Need to implement this method in the GenericRepository
       // return Ok(await genericRepository.GetBrandsAsync());
        return Ok();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        //Need to implement this method in the GenericRepository
        //return Ok(await genericRepository.GetTypesAsync());
        return Ok();
    }
}
