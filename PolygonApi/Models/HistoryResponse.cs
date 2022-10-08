using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record HistoryResponse
{
    [JsonConstructor]
    public HistoryResponse(List<HistoryRecord> historyRecords) => History = historyRecords;

    public List<HistoryRecord> History { get; }
}