#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MoretechBack.Database.Models;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Order
{
    protected Order() {}
    
    public Order(
        Product product, 
        int count, 
        string phoneNumber, 
        string address,
        string? color = null, 
        string? size = null
    )
    {
        Product = product;
        Count = count;
        PhoneNumber = phoneNumber;
        Address = address;
        Status = OrderStatus.InProcessing;
        SendDate = DateTime.Now;
        Color = color;
        Size = size;
    }
    
    public Guid Id { get; protected set; }

    public Product Product { get; protected set; }

    public int Count { get; protected set; }
    
    public User Owner { get; protected set; }
    
    [JsonIgnore]
    private Guid OwnerId { get; set; }
    
    public string PhoneNumber { get; protected set; }
    
    public string Address { get; protected set; }
    
    public OrderStatus Status { get; protected set; }
    
    public DateTime SendDate { get; protected set; }
    
    public string? Color { get; protected set; }
    
    public string? Size { get; protected set; }

    [NotMapped, JsonIgnore] public double Cost => Count * Product.Price;
}