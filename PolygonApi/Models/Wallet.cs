using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public class Wallet
{
    [JsonConstructor]
    public Wallet(string publicKey, string privateKey)
    {
        PublicKey = publicKey;
        PrivateKey = privateKey;
    }
    
    public string PublicKey { get; protected set; }
    
    public string PrivateKey { get; protected set; }
}