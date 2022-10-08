using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Notification
{
    public Guid Id { get; set; }
    
    public User Owner { get; set; } = null!;
    
    [JsonIgnore]
    private Guid OwnerId { get; set; }
    
    public string ShortDescription { get; set; } = null!;
    
    public string FullDescription { get; set; } = null!;

    public bool Read { get; set; } = false;

    public List<NotificationStatus> NotificationStatus { get; } = new();
}