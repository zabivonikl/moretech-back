using System.Text.Json.Serialization;

namespace MoretechBack.Controllers.ModelWrappers;

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
        UserId = userId;
        ProductId = productId;
        Count = count;
        Address = address;
        PhoneNumber = phoneNumber;
        Color = color;
        Size = size;
    }
    
    public string UserId { get; }
    
    public string ProductId { get; }

    public int Count { get; }
    
    public string Address { get; }
    
    public string PhoneNumber { get; }

    public string? Color { get; }

    public string? Size { get; }
}