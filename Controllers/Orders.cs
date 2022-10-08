using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("products/")]
public class Orders : Controller
{
    private readonly ConnectionsContext context;

    public Orders(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(string userId)
    {
        if (!Guid.TryParse(userId, out var parsedId)) 
            return BadRequest();

        var users = context.Users
            .Include(user => user.Orders)
            .ThenInclude(order => order.Products);
        
        var user = await users.FirstOrDefaultAsync(u => u.Id == parsedId);
        if (user == null)
            return BadRequest();
        
        return Json(user.Orders);
    }
}