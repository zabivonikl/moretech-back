using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class GlobalAchievement
{
    protected GlobalAchievement() { }

    [JsonConstructor]
    public GlobalAchievement(string title, string description, int total)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Total = total;
    }
    
    public Guid Id { get; protected set; }

    public string Title { get; protected set; } = null!;

    public string Description { get; protected set; } = null!;

    public int Total { get; protected set; }
}