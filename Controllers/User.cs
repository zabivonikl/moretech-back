﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database;
using MoretechBack.Database.Models;
using MoretechBack.PolygonApi;

namespace MoretechBack.Controllers;

[Authorize]
[ApiController]
[Route("user/")]
public class User : Controller
{
    private readonly ConnectionsContext context;

    private readonly IPolygonApiClient polygonClient;

    public User(ConnectionsContext context, IPolygonApiClient polygonClient)
    {
        this.context = context;
        this.polygonClient = polygonClient;
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        if (!Guid.TryParse(id, out var parsedId)) 
            return BadRequest();
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == parsedId);
        if (user == null)
            return BadRequest();
        
        var rawTransactions = await polygonClient.GetHistory(user);

        var userData = new
        {
            Fullname = user.FullName,
            user.Email,
            user.Avatar,
            Balance = await polygonClient.GetRubleBalance(user),
            Role = TranslateRole(user.Role),
            Transactions = rawTransactions.Select(async transaction => new
            {
                From = (await context.Users.FirstOrDefaultAsync(u => u.PublicKey.ToLower() == transaction.From.ToLower()))?.FullName ?? "Unknown user",
                To = (await context.Users.FirstOrDefaultAsync(u => u.PublicKey.ToLower() == transaction.To.ToLower()))?.FullName ?? "Unknown user",
                transaction.Value,
                transaction.TokenId,
                Time = transaction.TimeStamp,
                Currency = transaction.TokenName
            }).Select(t => t.Result),
            user.Achievements
        };
        
        return Json(userData);
    }

    private static string TranslateRole(Role role) => role switch
    {
        Role.Administrator => "Администратор",
        Role.User => "Пользователь",
        Role.Director => "Руководитель",
        Role.Leader => "Капитан",
        Role.Intern => "Стажёр",
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
}