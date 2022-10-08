using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record Nft
{
    [JsonConstructor]
    public Nft(long tokenId, string uri, string publicKey)
    {
        TokenId = tokenId;
        Uri = uri;
        PublicKey = publicKey;
    }

    public long TokenId { get; }
    
    public string Uri { get; }

    public string PublicKey { get; }
}