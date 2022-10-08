using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record TransactionStatus
{
    [JsonConstructor]
    public TransactionStatus(string status) =>
        Status = status;

    public string Status { get; }
}