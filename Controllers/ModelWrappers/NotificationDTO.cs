using System.Text.Json.Serialization;

namespace MoretechBack.Controllers.ModelWrappers;

public class NotificationDTO
{
    [JsonConstructor]
    public NotificationDTO(
        string owner,
        string shortDescription,
        string fullDescription
    )
    {
        Owner = owner;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
    }

    public string Owner { get; }

    public string ShortDescription { get; }
    
    public string FullDescription { get; }
}