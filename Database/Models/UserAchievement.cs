using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class UserAchievement
{
    protected UserAchievement()
    {
    }

    public UserAchievement(User owner, GlobalAchievement globalAchievement)
    {
        Owner = owner;
        GlobalAchievement = globalAchievement;
        Current = 0;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; protected set; }
    
    public GlobalAchievement GlobalAchievement { get; protected set; }
    
    private Guid GlobalAchievementId { get; set; }
    
    public User Owner { get; protected set; } = null!;
    
    private Guid OwnerId { get; set; }

    public int Current { get; protected set; }
}