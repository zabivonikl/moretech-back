using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Controllers.ModelWrappers;
using MoretechBack.Database;
using NotificationModel = MoretechBack.Database.Models.Notification;

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
        if (user == null)
            return BadRequest();

        foreach (var notification in user.Notification)
        {
            notification.Read = true;
        }
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(NotificationDTO notificationDto)
    {
        var user = await context.Users.Include(user => user.Notification).FirstOrDefaultAsync(u => u.Id == Guid.Parse(notificationDto.Owner));
        if (user == null)
            return BadRequest();
        var notification = new NotificationModel
        {
            Read = false, 
            FullDescription = notificationDto.FullDescription,
            ShortDescription = notificationDto.ShortDescription
        };
        user.Notification.Add(notification);
        await context.SaveChangesAsync();

        return Ok();
    }
}