using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public class TransactionResponse
{
    [JsonConstructor]
    public TransactionResponse(string transactionHash) =>
        TransactionHash = transactionHash;

    public string TransactionHash { get; }
}