using MoretechBack.Database.Models;
using MoretechBack.PolygonApi.Models;

namespace MoretechBack.PolygonApi;

public interface IPolygonApiClient
{
    Task<List<HistoryRecord>> GetHistory(User user);

    Task<double> GetRubleBalance(User user);

    Task DecreaseMoney(User user, double amount);
}