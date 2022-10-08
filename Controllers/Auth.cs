using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoretechBack.Auth;
using MoretechBack.Database;

namespace MoretechBack.Controllers;

[ApiController]
[Route("auth/")]
public class Auth : Controller
{
    private readonly ConnectionsContext context;

    public Auth(ConnectionsContext context)
    {
        this.context = context;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Token(string login, string password)
    {
        var userGuid = await GetUser(login, password);
        if (userGuid == null) 
            return Unauthorized();

        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        string? encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        return Json(new { Token = encodedJwt, Id = userGuid });
    }
    
    private async Task<Guid?> GetUser(string login, string password)
    {
        var user = context.Users.FirstOrDefault(user => user.Email == login && user.Password == password);
        if (user == null)
            return null;
        
        user.LastLogin = DateTime.Now;
        await context.SaveChangesAsync();
        return user.Id;
    }
}