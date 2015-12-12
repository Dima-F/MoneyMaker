using System.Collections.Generic;
using HandHistories.Parser.MoneyMaker.BLL.ViewEntities;

namespace HandHistories.Parser.MoneyMaker.BLL.Repositories
{
    public interface IRepository
    {
        IEnumerable<string> OponentNames { get;}
        IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name);
        IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name);
    }
}
