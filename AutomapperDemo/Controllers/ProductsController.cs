using AutoMapper;
using AutomapperDemo.Data;
using AutomapperDemo.DTOs;
using AutomapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomapperDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly IMapper _mapper;

    public ProductsController ( ProductDbContext context, IMapper mapper )
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts ()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();

        var productDTO = _mapper.Map<List<ProductDTO>>(products);

        return Ok(productDTO);
    }

    [HttpGet("GetProductbyId/{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductbyId ( int id )
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(prd => prd.Id == id);

        if (product == null)
            return NotFound();

        var productDTO = _mapper.Map<ProductDTO>(product);

        return Ok(productDTO);
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<ProductDTO>> AddProduct ( ProductCreateDTO productCreateDTO )
    {
        if (productCreateDTO == null)
            return BadRequest("Invalid product data.");

        var product = _mapper.Map<Product>(productCreateDTO);

        product.IsAvailable = product.StockQuantity > 0;
        product.CreatedDate = DateTime.Now;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Now that the product.Id is generated, create the SKU using the new logic:
        product.SKU = GenerateSKU(product);
        // Update the product record with the new SKU
        await _context.SaveChangesAsync();

        var productDTO = _mapper.Map<ProductDTO>(product);
        return Ok(productDTO);
    }



    // The SKU would be "ELE-SAM-GAL-2025-15".
    private string GenerateSKU ( Product product )
    {
        // Use default values if any fields are missing
        string category = string.IsNullOrEmpty(product.Category) ? "GEN" : product.Category;
        string brand = string.IsNullOrEmpty(product.Brand) ? "BRD" : product.Brand;
        string name = string.IsNullOrEmpty(product.Name) ? "PRD" : product.Name;
        // Extract the first three letters of each, padding if necessary
        string catPrefix = category.Length >= 3
            ? category.Substring(0, 3).ToUpper()
            : category.ToUpper().PadRight(3, 'X');
        string brandPrefix = brand.Length >= 3
            ? brand.Substring(0, 3).ToUpper()
            : brand.ToUpper().PadRight(3, 'X');
        string prodPrefix = name.Length >= 3
            ? name.Substring(0, 3).ToUpper()
            : name.ToUpper().PadRight(3, 'X');
        // Use the year from CreatedDate and the generated Id
        int year = product.CreatedDate.Year;
        int id = product.Id;
        // Assemble SKU with hyphen separators
        return $"{catPrefix}-{brandPrefix}-{prodPrefix}-{year}-{id}";
    }

}
