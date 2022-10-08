using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class User
{
    protected User() { }
    
    public Guid Id { get; protected set; }
    
    public string FullName { get; protected set; } = null!;

    public string Email { get; protected set; } = null!;

    public string Password { get; protected set; } = null!;

    public string Avatar { get; protected set; } = null!;

    public string PublicKey { get; protected set; } = null!;

    public string PrivateKey { get; protected set; } = null!;

    public List<Achievement> Achievements { get; set; } = null!;

    public List<Notification> Notification { get; set; } = null!;
}