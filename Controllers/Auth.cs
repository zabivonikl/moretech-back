using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret phaseSecret phaseSecret phaseSecret phaseSecret phaseSecret phase"));
        
        var jwt = new JwtSecurityToken(
            expires: now.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
        );
        string? encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        return Json(new { Token = encodedJwt, Id = userGuid });
    }
    
    private async Task<Guid?> GetUser(string login, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == login && user.Password == password);
        if (user == null)
            return null;
        
        user.LastLogin = DateTime.Now;
        await context.SaveChangesAsync();
        return user.Id;
    }
}