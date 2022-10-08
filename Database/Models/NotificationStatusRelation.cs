using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class NotificationStatusRelation
{
    public Guid Id { get; protected set; }

    public Notification Notification { get; protected set; } = null!;

    public NotificationStatus NotificationStatus { get; protected set; } = null!;
}