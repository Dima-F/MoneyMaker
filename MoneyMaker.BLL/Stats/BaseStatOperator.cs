using System.Collections.Generic;
using System.Linq;
using HandHistories.SimpleObjects.Entities;
using MoneyMaker.BLL.Tools;

namespace MoneyMaker.BLL.Stats
{
    /// <summary>
    /// Ф:Формирует коллекцию статов, используя файл конфигурации
    /// </summary>
    public class BaseStatOperator : IStatOperator
    {
        public List<PlayerStats> GetPlayerStatsList(IEnumerable<Game> games)
        {
            var gs = games as Game[] ?? games.ToArray();
            var last3GamesPlayerNames = gs.GetLast3GamesPlayerNames();
            return last3GamesPlayerNames.Select(playerName => GetPlayerStats(playerName, gs)).ToList();
        }
        private PlayerStats GetPlayerStats(string playerName, IEnumerable<Game> games)
        {
            var statCollection = new PlayerStats(playerName);
            var playerGames = games.GetGamesForPlayer(playerName).ToList();
            var gamesCount = playerGames.Count();
            var handsWon = playerGames.GetHandsWonCountForPlayerGames(playerName);
            statCollection.Add(new Stat() { Name = "Win %", Value = decimal.Round((decimal)handsWon / (decimal)gamesCount * 100, 2) });
            statCollection.Add(new Stat() { Name = "Hands", Value = gamesCount });
            var valPutCount = playerGames.VPIPCountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "VPIP", Value = decimal.Round((decimal)valPutCount / (decimal)gamesCount * 100, 2) });
            var profit = playerGames.CalculateTotalProfit(playerName);
            statCollection.Add(new Stat() { Name = "Profit", Value = decimal.Round((decimal)profit, 2) });
            var preflopRaiseCount = playerGames.PFRCountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "PFR", Value = decimal.Round((decimal)preflopRaiseCount / (decimal)gamesCount * 100, 2) });
            var atsPercent = playerGames.GetATSPercentForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "BB", Value = decimal.Round(playerGames.Last().GetBBForPlayer(playerName)) });
            statCollection.Add(new Stat() { Name = "ATS", Value = decimal.Round(atsPercent, 2) });
            var afpfPercent = playerGames.GetAFPercentForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "AF", Value = decimal.Round((decimal)afpfPercent, 2) });
            var preflop3BCount = playerGames.Get3BCountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "3B", Value = decimal.Round((decimal)preflop3BCount / (decimal)gamesCount * 100, 2) });
            return statCollection;
        }
    }
}
