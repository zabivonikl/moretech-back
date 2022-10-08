using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoretechBack.Database;

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
}