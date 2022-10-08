using System.Text.Json.Serialization;

namespace MoretechBack.Controllers;

public class OrderDto
{
    [JsonConstructor]
    public OrderDto(
        string userId, 
        string productId,
        int count,
        string address, 
        string phoneNumber,
        string? color = null,
        string? size = null)
    {
        UserId = Guid.Parse(userId);
        ProductId = Guid.Parse(productId);
        Count = count;
        Color = color;
        Size = size;
        Address = address;
        PhoneNumber = phoneNumber;
    }
    
    public Guid UserId { get; }
    
    public Guid ProductId { get; }

    public int Count { get; }
    
    public string Address { get; }
    
    public string PhoneNumber { get; }

    public string? Color { get; }

    public string? Size { get; }
}