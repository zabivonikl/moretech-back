using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record HistoryResponse
{
    [JsonConstructor]
    public HistoryResponse(List<HistoryRecord> history) => History = history;

    public List<HistoryRecord> History { get; }
}