using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoretechBack.Database;
using MoretechBack.Database.Models;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class Colleagues : Controller
{
    private readonly ConnectionsContext context;

    public Colleagues(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public Task<IActionResult> Get(string fullname = "")
    {
        var users = context.Users.Where(user => user.FullName.Contains(fullname)).ToList();
        var usersData = users.Select(user => new
        {
            user.FullName,
            user.Avatar,
            Description = $"{user.Division}, {user.Post}",
            Role = user.Role.ToString()
        });
        return Task.FromResult<IActionResult>(Json(usersData));
    }
}