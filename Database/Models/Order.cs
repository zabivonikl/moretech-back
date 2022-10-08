#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class Order
{
    protected Order() {}
    
    public Guid Id { get; protected set; }

    public List<Product> Products { get; } = new();
    
    public User Owner { get; protected set; }
    
    private Guid OwnerId { get; set; }
    
    public string Address { get; protected set; }
    
    public OrderStatus Status { get; protected set; }
    
    public DateTime SendDate { get; protected set; }
}