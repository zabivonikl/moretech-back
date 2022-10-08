using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record HistoryRecord
{
    [JsonConstructor]
    public HistoryRecord(
        long blockNumber, 
        long timestamp, 
        string contractAddress, 
        string from, 
        string to, 
        long value, 
        long tokenId, 
        string tokenName, 
        string tokenSymbol, 
        long gasUsed, 
        long confirmations)
    {
        BlockNumber = blockNumber;
        Timestamp = timestamp;
        ContractAddress = contractAddress;
        From = from;
        To = to;
        Value = value;
        TokenId = tokenId;
        TokenName = tokenName;
        TokenSymbol = tokenSymbol;
        GasUsed = gasUsed;
        Confirmations = confirmations;
    }

    public long BlockNumber { get; }
    
    public long Timestamp { get; }
    
    public string ContractAddress { get; }
    
    public string From { get; }
    
    public string To { get; }
    
    public long Value { get; }
    
    public long TokenId { get; }
    
    public string TokenName { get; }
    
    public string TokenSymbol { get; }
    
    public long GasUsed { get; }
    
    public long Confirmations { get; }
}