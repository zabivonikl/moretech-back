using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoretechBack.Database;
using MoretechBack.Database.Models;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("products/")]
public class Products : Controller
{
    private readonly ConnectionsContext context;

    public Products(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpGet("get")]
    public Task<IActionResult> Get() => 
        Task.FromResult<IActionResult>(Json(context.Products.ToList()));

    [HttpPost("add")]
    public async Task<IActionResult> Create(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return Ok();
    }
}