using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record GeneratedNftTransaction
{
    public GeneratedNftTransaction(List<long> tokens, string walletId)
    {
        Tokens = tokens;
        WalletId = walletId;
    }

    [JsonPropertyName("wallet_id")]
    public string WalletId { get; }

    public List<long> Tokens { get; }
}