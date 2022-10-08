using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Controllers.ModelWrappers;
using MoretechBack.Database;
using MoretechBack.Database.Models;
using MoretechBack.PolygonApi;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("orders/")]
public class Orders : Controller
{
    private readonly ConnectionsContext context;
    
    private readonly IPolygonApiClient client;

    public Orders(ConnectionsContext context, IPolygonApiClient client)
    {
        this.context = context;
        this.client = client;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(string userId)
    {
        if (!Guid.TryParse(userId, out var parsedId)) 
            return BadRequest();

        var users = context.Users
            .Include(user => user.Orders)
            .ThenInclude(order => order.Product);
        
        var user = await users.FirstOrDefaultAsync(u => u.Id == parsedId);
        if (user == null)
            return BadRequest();
        
        return Json(user.Orders);
    }

    [HttpPost("new")]
    public async Task<IActionResult> NewOrder(OrderDto orderData)
    {
        var customer = await context.Users.FirstOrDefaultAsync(user => user.Id == Guid.Parse(orderData.UserId));
        if (customer == null) 
            return BadRequest();
        
        var product = await context.Products.FirstOrDefaultAsync(product => product.Id == Guid.Parse(orderData.ProductId));
        if (product == null) 
            return BadRequest();

        try
        {
            var order = new Order(
                product,
                orderData.Count,
                orderData.PhoneNumber,
                orderData.Address,
                orderData.Color,
                orderData.Size
            );
            await client.DecreaseMoney(customer, order.Cost);
            customer.Orders.Add(order);
            await context.SaveChangesAsync();
        }
        catch (InvalidOperationException err)
        {
            return Json(new { Error = err.Message });
        }

        return Ok();
    }
}