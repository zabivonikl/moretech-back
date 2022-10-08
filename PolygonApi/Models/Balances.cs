using System.Text.Json.Serialization;

namespace MoretechBack.PolygonApi.Models;

public record Balances
{
    [JsonConstructor]
    public Balances(double maticAmount, double coinsAmount)
    {
        MaticAmount = maticAmount;
        CoinsAmount = coinsAmount;
    }

    public double MaticAmount { get; }
    
    public double CoinsAmount { get; }
}