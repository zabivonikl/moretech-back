using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoretechBack.Database;
using MoretechBack.PolygonApi;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class User : Controller
{
    private readonly ConnectionsContext context;

    private readonly IPolygonApiClient polygonClient;

    public User(ConnectionsContext context, IPolygonApiClient polygonClient)
    {
        this.context = context;
        this.polygonClient = polygonClient;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        if (!Guid.TryParse(id, out var parsedId)) 
            return BadRequest();
        
        var user = context.Users.FirstOrDefault(u => u.Id == parsedId);
        if (user == null)
            return BadRequest();
        
        var rawTransactions = await polygonClient.GetHistory(user);

        var userData = new
        {
            Fullname = user.FullName,
            user.Email,
            user.Avatar,
            Balance = await polygonClient.GetRubleBalance(user),
            Transactions = rawTransactions.Select(transaction => new
            {
                From = context.Users.FirstOrDefault(u => u.PublicKey.ToLower() == transaction.From.ToLower())?.FullName ?? "Unknown user",
                To = context.Users.FirstOrDefault(u => u.PublicKey.ToLower() == transaction.To.ToLower())?.FullName ?? "Unknown user",
                transaction.Value,
                transaction.TokenId,
                Time = transaction.TimeStamp,
                Currency = transaction.TokenName
            }),
            user.Achievements
        };
        
        return Json(userData);
    }
}