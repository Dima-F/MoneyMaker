using System.Collections.Generic;

namespace HandHistories.Parser.MoneyMaker.Repositories
{
    public interface IRepository
    {
        IEnumerable<string> OponentNames { get;}
        IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name);
        IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name);
    }
}
