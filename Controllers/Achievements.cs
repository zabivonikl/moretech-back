using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database;
using MoretechBack.Database.Models;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("achievements/")]
public class Achievements : Controller
{
    private readonly ConnectionsContext context;

    public Achievements(ConnectionsContext context)
    {
        this.context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGlobalAchievement(GlobalAchievement globalAchievement)
    {
        var achievement = context.Achievements.Add(new GlobalAchievement(
            globalAchievement.Title, 
            globalAchievement.Description, 
            globalAchievement.Total
        ));

        foreach (var user in context.Users)
            await context.AddAsync(new UserAchievement(user, achievement.Entity));

        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("getAll")]
    public Task<IActionResult> GetAll() =>
        Task.FromResult<IActionResult>(Json(context.Achievements.ToList()));

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAll(string id)
    {
        if (!Guid.TryParse(id, out var parsedId)) 
            return BadRequest();

        var users = context.Users
            .Include(user => user.Achievements)
            .ThenInclude(order => order.GlobalAchievement);
        
        var user = await users.FirstOrDefaultAsync(u => u.Id == parsedId);
        if (user == null)
            return BadRequest();
        
        return Json(user.Achievements.Select(achievement => new
        {
            achievement.GlobalAchievement.Id,
            achievement.GlobalAchievement.Title,
            achievement.GlobalAchievement.Description,
            achievement.GlobalAchievement.Total,
            achievement.Current,
        }).ToList());
    }
}