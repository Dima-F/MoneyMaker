using System.Collections.Generic;
using HandHistories.SimpleObjects.Entities;

namespace MoneyMaker.BLL.Stats
{
    public interface IStatOperator
    {
        List<PlayerStats> GetPlayerStatsList(IEnumerable<Game> games);
    }
}