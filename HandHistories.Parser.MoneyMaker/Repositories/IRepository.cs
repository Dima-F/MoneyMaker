using System.Collections.Generic;
using MoneyMaker.BLL.ViewEntities;

namespace HandHistories.Parser.MoneyMaker.Repositories
{
    public interface IRepository
    {
        IEnumerable<string> OponentNames { get;}
        IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name);
        IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name);
    }
}
