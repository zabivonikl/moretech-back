using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record NftBalanceItem
{
    [JsonConstructor]
    public NftBalanceItem(string uri, List<long> tokens)
    {
        Uri = uri;
        Tokens = tokens;
    }

    [JsonPropertyName("URI")]
    public string Uri { get; }
    
    public List<long> Tokens { get; }
}