namespace MoretechBack.Database.Models;

public enum OrderStatus : byte
{
    InProcessing,
    
    IsSent,
    
    InTransit,
    
    Archived,
}