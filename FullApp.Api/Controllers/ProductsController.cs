using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FullApp.Api.Data;
using FullApp.Api.Entities;
using System.Linq;

// Basic CRUD controller for products.
// Owner/admin logic and validation are minimal and can be enhanced.
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    // GET api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _db.Products.ToListAsync();
        return Ok(products);
    }

    // GET api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    // POST api/products (requires authorization)
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product model)
    {
        // For demo, CreatedByUserId not validated.
        _db.Products.Add(model);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // PUT api/products/{id}
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product model)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = model.Name;
        existing.Description = model.Description;
        existing.Price = model.Price;
        existing.Stock = model.Stock;
        existing.UpdatedAt = System.DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/products/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing == null) return NotFound();

        _db.Products.Remove(existing);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
