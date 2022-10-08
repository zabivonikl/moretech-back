using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record HistoryRecord
{
    [JsonConstructor]
    public HistoryRecord(
        string hash,
        long blockNumber, 
        long timeStamp, 
        string contractAddress, 
        string from, 
        string to, 
        string tokenName, 
        string tokenSymbol, 
        long gasUsed, 
        long confirmations,
        long? value = null,
        long? tokenId = null)
    {
        Hash = hash;
        BlockNumber = blockNumber;
        TimeStamp = timeStamp;
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

    public string Hash { get; }

    public long BlockNumber { get; }
    
    public long TimeStamp { get; }
    
    public string ContractAddress { get; }
    
    public string From { get; }
    
    public string To { get; }
    
    public long? Value { get; }
    
    public long? TokenId { get; }
    
    public string TokenName { get; }
    
    public string TokenSymbol { get; }
    
    public long GasUsed { get; }
    
    public long Confirmations { get; }
}