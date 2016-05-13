using System;
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
            throw new NotImplementedException("Some stats are not implemented.");
            var statCollection = new PlayerStats(playerName);
            var playerGames = games.GetGamesForPlayer(playerName).ToList();
            var gamesCount = playerGames.Count();
            var handsWon = playerGames.GetHandsWonCountForPlayerGames(playerName);
            statCollection.Add(new Stat() { Name = "Win %", Value = Math.Round((double)handsWon / gamesCount * 100, 2) });
            statCollection.Add(new Stat() { Name = "Hands", Value = gamesCount });
            var valPutCount = playerGames.VPIP_CountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "VPIP", Value = Math.Round((double)valPutCount / gamesCount * 100, 2) });
            var profit = playerGames.CalculateTotalProfit(playerName);
            statCollection.Add(new Stat() { Name = "Profit", Value = Math.Round(profit, 2) });
            var preflopRaiseCount = playerGames.PFR_CountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "PFR", Value = Math.Round((double)preflopRaiseCount /gamesCount * 100, 2) });
            var atsPercent = playerGames.ATS_PercentForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "BB", Value = Math.Round(playerGames.Last().BB_ForPlayer(playerName)) });
            statCollection.Add(new Stat() { Name = "ATS", Value = Math.Round(atsPercent, 2) });
            var afpfPercent = playerGames.AF_ForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "AF", Value = Math.Round(afpfPercent, 2)});
            var preflop3BCount = playerGames.ThreeBetCountForPlayer(playerName);
            statCollection.Add(new Stat() { Name = "3B", Value = Math.Round((double)preflop3BCount / gamesCount * 100, 2) });
            return statCollection;
        }
    }
}
