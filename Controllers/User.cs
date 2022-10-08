using Microsoft.AspNetCore.Mvc;
using MoretechBack.Database;

namespace MoretechBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class User : Controller
{
    private readonly ConnectionsContext context;

    public User(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpGet("{id}")]
    public async Task Get(string id)
    {
        
    }
}