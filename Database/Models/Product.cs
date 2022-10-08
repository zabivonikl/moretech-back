#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Product
{
    protected Product()
    {
    }
    
    [JsonConstructor]
    public Product(string title, List<string> images, double price) : 
        this(Guid.NewGuid(), title, images, price)
    {
        Title = title;
        Images = images.ToList();
        Price = price;
    }

    public Product(Guid id, string title, List<string> images, double price)
    {
        Id = id;
        Title = title;
        Images = images.ToList();
        Price = price;
    }
    
    public Guid Id { get; protected set; }
    
    public string Title { get; protected set; }
    
    public List<string> Images { get; protected set; }
    
    public double Price { get; protected set; }
}