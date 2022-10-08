using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("read")]
    public string[] Get()
    {
        //TODO реализовать прочтение ачивки
        return new string[] {"hello"};
    }
}