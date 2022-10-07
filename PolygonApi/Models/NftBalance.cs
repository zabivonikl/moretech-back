using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public class NftBalance
{
    [JsonConstructor]
    public NftBalance(List<NftBalanceItem> balance)
    {
        Balance = balance;
    }

    public List<NftBalanceItem> Balance { get; }
}