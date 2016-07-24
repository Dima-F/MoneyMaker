using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.BLL.Stats
{
    public abstract class StatOperator
    {
        private readonly bool _useOnlyLive;
        private readonly int _lastGames;
        protected StatOperator(bool useOnlyLive, int lastGames)
        {
            _useOnlyLive = useOnlyLive;
            _lastGames = lastGames;
        }

        public virtual List<PlayerStats> GetPlayerStatsList(IEnumerable<Game> games)
        {
            var gs = games as Game[] ?? games.ToArray();
            var playerNames = !_useOnlyLive ? gs.GetAllPlayerNames() : gs.GetLivePlayerNames(_lastGames);
            return playerNames.Select(playerName => GetPlayerStats(playerName, gs)).ToList();
        }

        protected abstract PlayerStats GetPlayerStats(string playerName, IEnumerable<Game> games);
    }
}