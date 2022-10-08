using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoretechBack.Auth;
using MoretechBack.Database;

namespace MoretechBack.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Auth : Controller
{
    private readonly ConnectionsContext context;

    public Auth(ConnectionsContext context)
    {
        this.context = context;
    }
    
    [HttpPost("/login")]
    public IActionResult Token(string login, string password)
    {
        if (!IsUserDataCorrect(login, password)) return Unauthorized();

        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        string? encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        return Json(encodedJwt);
    }
    
    private bool IsUserDataCorrect(string login, string password)
    {
        var user = context.Users.FirstOrDefault(user => user.Email == login && user.Password == password);
        return user != null;
    }
}