using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.SimpleObjects.Entities;
using HandHistories.SimpleObjects.Tools;
using HandHistories.SimpleParser;
using MoneyMaker.BLL.Files;
using MoneyMaker.BLL.Stats;
using MoneyMaker.BLL.Tools;
using MoneyMaker.BLL.ViewEntities;

namespace MoneyMaker.BLL.Hud
{

    /// <summary>
    /// Ф:Клас-парсер в режиме реального времени с одновременным возвратом необходимых статистических данных 
    /// без сохранения в базе данных. Т.е. он использует только данные текущей сессии. 
    /// </summary>
    public class HudInitializer : IHudInitializer
    {
        private readonly List<Game> _games;

        private readonly List<HandAction> _allHandActions;

        public HudInitializer(IHandHistoryParser parser, string path)
        {
            Path = path;
            _games = parser.ParseGames(PokerFileReader.ReadFileWithWaiting(Path));
            _allHandActions = new List<HandAction>();
        }

        public string Path { get; private set; }

        //Lazy load pattern
        public List<HandAction> AllHandActions
        {
            get
            {
                if (_allHandActions.Count != 0)
                    return _allHandActions;
                foreach (var game in _games)
                {
                    _allHandActions.AddRange(game.HandActions);
                }
                return _allHandActions;
            }
        }

        public List<PlayerStats> ParseHudStats()
        {
            var last3GamesPlayerNames = _games.GetLast3GamesPlayerNames();
            return last3GamesPlayerNames.Select(GetStatCollection).ToList();
        }

        public string GetHudInfo()
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
            var builder = new StringBuilder();
            builder.AppendLine("Date: " + fileName.GetDateFromFileName().ToShortDateString());
            builder.AppendLine("Table name: " + fileName.GetTableFromFileName());
            builder.AppendLine("Limit: " + fileName.GetLimitFromFileName());
            builder.AppendLine("Blinds: " + fileName.GetBlindsFromFileName());
            builder.AppendLine("Played hands: " + _games.Count);
            builder.AppendLine("Sitting for: " + ParseTimeSession() + " minutes");
            return builder.ToString();
        }

        public string GetHeroCards()
        {
            var heroCards = _games.GetHeroCardsFromLastGame();
            return heroCards != null ? heroCards.ConvertByteCardsToString() : string.Empty;
        }

        public Muck GetMucking()
        {
            var lastGame = _games.Last();
            return lastGame.WasMucking() ? lastGame.GetMuck() : null;
        }

        public IEnumerable<decimal> GetHeroProfits()
        {
            return _games.Select(game => game.CalculateHeroProfit());
        }





        private int ParseTimeSession()
        {
            TimeSpan start = _games.First().DateOfHand.TimeOfDay;
            TimeSpan end = _games.Last().DateOfHand.TimeOfDay;
            if (end >= start)
                return Convert.ToInt32((end - start).TotalMinutes);
            var timeBefore = (new TimeSpan(23, 59, 59)) - start;
            return Convert.ToInt32((timeBefore + end).TotalMinutes);
        }

        private PlayerStats GetStatCollection(string playerName)
        {
            var playerGames = _games.GetGamesForPlayer(playerName).ToList();
            var handsWon = playerGames.GetHandsWonCountForPlayerGames(playerName);
            var gamesCount = playerGames.Count();
            var valPutCount = playerGames.VPIPCountForPlayer(playerName);
            var preflopRaiseCount = playerGames.PFRCountForPlayer(playerName);
            var preflop3BCount = playerGames.Get3BCountForPlayer(playerName);
            var atsPercent = playerGames.GetATSPercentForPlayer(playerName);
            var afpfPercent = playerGames.GetAFPercentForPlayer(playerName);
            var profit = playerGames.CalculateTotalProfit(playerName);
            var statCollection = new PlayerStats(playerName, gamesCount)
            {
                new Stat
                {
                    Name = "Win %",
                    Value = decimal.Round((decimal) handsWon/(decimal) gamesCount*100, 2)
                },
                new Stat
                {
                    Name = "Profit",
                    Value = decimal.Round((decimal) profit, 2)
                },
                new Stat
                {
                    Name = "VPIP",
                    Value = decimal.Round((decimal) valPutCount/(decimal) gamesCount*100, 2)
                },
                new Stat()
                {
                    Name = "PFR",
                    Value = decimal.Round((decimal) preflopRaiseCount/(decimal) gamesCount*100, 2)
                },
                new Stat
                {
                    Name = "ATS",
                    Value = decimal.Round(atsPercent, 2)
                },
                new Stat()
                {
                    Name = "AF",
                    Value = decimal.Round((decimal) afpfPercent, 2)
                },
                new Stat()
                {
                    Name = "3B",
                    Value = decimal.Round((decimal) preflop3BCount/(decimal) gamesCount*100, 2)
                }
            };
            return statCollection;
        }

    }
}
