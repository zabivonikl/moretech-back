using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class NotificationStatus
{
    public Guid Id { get; protected set; }
    
    public string Name { get; protected set; } = null!;

    public List<NotificationStatusRelation> NotificationStatusRelations = null!;
}