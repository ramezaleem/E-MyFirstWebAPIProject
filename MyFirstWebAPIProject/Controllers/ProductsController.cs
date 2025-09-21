using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1000.00m, Category = "Electronics" },
        new Product { Id = 2, Name = "Desktop", Price = 2000.00m, Category = "Electronics" },
        new Product { Id = 3, Name = "Mobile", Price = 300.00m, Category = "Electronics" },
        // Additional products can be added here
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts ()
    {
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct ( int id )
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product is null)
            return NotFound(new { Message = $"Product with ID {id} not found." });

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> PostProduct ( [FromBody] Product product )
    {
        product.Id = _products.Max(p => p.Id) + 1;

        _products.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult PutProduct ( int id, [FromBody] Product updateProduct )
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);

        if (id != updateProduct.Id)
            return BadRequest(new { Message = "ID mismatch between route and body." });
        if (existingProduct is null)
            return NotFound(new { Meassage = $"Product with ID {id} not found." });

        existingProduct.Name = updateProduct.Name;
        existingProduct.Price = updateProduct.Price;
        existingProduct.Category = updateProduct.Category;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct ( int id )
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);

        if (existingProduct is null)
            return NotFound(new { Meassage = $"Product with ID {id} not found." });

        _products.Remove(existingProduct);
        return NoContent();
    }


}
