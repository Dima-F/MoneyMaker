using System.Collections.Generic;

namespace HandHistories.Parser.MoneyMaker.BLL
{
    public interface IRepositoryManager
    {
        IEnumerable<string> OponentNames { get;}
        IEnumerable<PlayerSummary> GetPlayerSummariesByName(string name);
        IEnumerable<PlayerStatistics> GetPlayerStatisticsByName(string name);
    }
}
