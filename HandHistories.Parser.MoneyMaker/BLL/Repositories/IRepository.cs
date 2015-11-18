using System.Collections.Generic;

namespace HandHistories.Parser.MoneyMaker.BLL.Repositories
{
    public interface IRepository
    {
        IEnumerable<string> OponentNames { get;}
        IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name);
        IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name);
    }
}
