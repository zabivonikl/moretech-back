using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database;

namespace MoretechBack.Controllers;

[ApiController]
[Route("notification/")]
public class Notification : Controller
{
    private readonly ConnectionsContext context;

    public Notification(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpGet("read/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        if (!Guid.TryParse(id, out var parsedId))
            return BadRequest();
        
        var user = await context.Users.Include(user => user.Notification).FirstOrDefaultAsync(u => u.Id == parsedId);
        if (user == null || user.Notification == null)
            return BadRequest();

        foreach (Database.Models.Notification notification in user.Notification)
        {
            notification.Read = true;
        }
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(Database.Models.Notification notification)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(notification.Owner));
        if (user == null)
            return BadRequest();
        user.Notification.Add(notification);
        await context.SaveChangesAsync();

        return Ok();
    }
}