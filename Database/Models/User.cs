using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class User
{
    protected User() { }
    
    public Guid Id { get; protected set; }
    
    public string FullName { get; protected set; } = null!;

    public string Email { get; protected set; } = null!;

    public string Password { get; protected set; } = null!;

    public string Avatar { get; protected set; } = null!;

    public Role Role { get; protected set; }

    public DateTime LastLogin { get; set; }

    public string Division { get; protected set; } = null!;

    public string Post { get; protected set; } = null!;

    public string PublicKey { get; protected set; } = null!;

    public string PrivateKey { get; protected set; } = null!;

    public List<Achievement> Achievements { get; } = new();

    public List<Notification> Notification { get; } = new();

    public List<Order> Orders { get; } = new();
}