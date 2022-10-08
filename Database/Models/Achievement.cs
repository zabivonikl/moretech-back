using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Achievement
{
    
    protected Achievement() { }
    
    public Guid Id { get; protected set; }
    
    public User Owner { get; protected set; } = null!;
    
    private Guid OwnerId { get; set; }
    
    public string Title { get; protected set; } = null!;

    public string Description { get; protected set; } = null!;

    public int Current { get; protected set; }

    public int Total { get; protected set; }
}