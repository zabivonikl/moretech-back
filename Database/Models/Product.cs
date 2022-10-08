#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class Product
{
    protected Product() {}
    
    public Guid Id { get; protected set; }
    
    public string Title { get; protected set; }
    
    public string[] Images { get; protected set; }
    
    public long Price { get; protected set; }
    
    public string Color { get; protected set; }
    
    public string Size { get; protected set; }
}