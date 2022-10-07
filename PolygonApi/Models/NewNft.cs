using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public class NewNft
{
    [JsonConstructor]
    public NewNft(string toPublicKey, string uri, int nftCount)
    {
        ToPublicKey = toPublicKey;
        Uri = uri;
        NftCount = nftCount;
    }

    public string ToPublicKey { get; }
    
    [JsonPropertyName("URI")]
    public string Uri { get; }
    
    public int NftCount { get; }
}