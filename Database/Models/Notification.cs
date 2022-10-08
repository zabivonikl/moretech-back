using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Notification
{
    public Guid Id { get; set; }
    
    public User Owner { get; set; } = null!;
    
    private Guid OwnerId { get; set; }
    
    public string ShortDescription { get; set; } = null!;
    
    public string FullDescription { get; set; } = null!;

    public bool Read { get; set; } = false;

    public List<NotificationStatusRelation> NotificationStatusRelations = null!;
}